import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  inject,
  signal,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { SelectModule } from 'primeng/select';
import { InputNumberModule } from 'primeng/inputnumber';
import { CheckboxModule } from 'primeng/checkbox';
import { DropdownModule } from 'primeng/dropdown';
import {
  JobPostingResponse,
  CreateJobPostingRequest,
  UpdateJobPostingRequest,
  JobCategory,
} from '../../../../core/api';
import { ValidationService } from '../../../../core/services/validation.service';
import { CompanyService } from '../../../../core/api';
import { TextareaModule } from 'primeng/textarea';
import { AutoCompleteCompleteEvent, AutoCompleteModule } from 'primeng/autocomplete';

interface CompanyOption {
  label: string;
  value: string;
}

@Component({
  selector: 'app-job-posting-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    TranslateModule,
    SelectModule,
    TextareaModule,
    InputNumberModule,
    CheckboxModule,
    DropdownModule,
    AutoCompleteModule,
  ],
  templateUrl: './job-posting-form.component.html',
  styleUrl: './job-posting-form.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class JobPostingFormComponent implements OnInit {
  @Input() jobPosting: JobPostingResponse | null = null;
  @Input() saving = false;
  @Input() isNew = false;

  @Output() jobPostingSaved = new EventEmitter<UpdateJobPostingRequest | CreateJobPostingRequest>();
  @Output() cancelEdit = new EventEmitter<void>();

  private fb = inject(FormBuilder);
  private validationService = inject(ValidationService);
  private translateService = inject(TranslateService);
  private companyService = inject(CompanyService);

  jobPostingForm: FormGroup;
  companies = signal<CompanyOption[]>([]);
  jobCategories = Object.values(JobCategory);

  constructor() {
    this.jobPostingForm = this.createForm();
    this.loadCompanies('');
  }

  ngOnInit(): void {
    if (this.jobPosting) {
      this.updateFormWithJobPosting(this.jobPosting);
    }
  }

  onSubmit(): void {
    if (this.jobPostingForm.invalid) {
      this.jobPostingForm.markAllAsTouched();
      return;
    }

    const formValue = this.jobPostingForm.getRawValue();
    this.jobPostingSaved.emit(formValue);
  }

  onCancel(): void {
    this.cancelEdit.emit();
  }

  isInvalid(controlName: string): boolean {
    const control = this.getControl(controlName);
    return this.validationService.hasError(control);
  }

  getErrorMessage(controlName: string, fieldName: string): string | null {
    const control = this.getControl(controlName);
    return this.validationService.getValidationMessage(
      control,
      this.translateService.instant(fieldName)
    );
  }

  search(event: AutoCompleteCompleteEvent) {
    this.loadCompanies(event.query);
  }

  private async loadCompanies(searchText: string): Promise<void> {
    try {
      const result = await this.companyService.getAllCompanies(1, 100, searchText).toPromise();
      const companyOptions = (result?.items || []).map((company) => ({
        label: company.name || '',
        value: company.id || '',
      }));
      this.companies.set(companyOptions);
    } catch (error) {
      console.error('Error loading companies', error); // TODO: fix this later
    }
  }

  private getControl(path: string): FormControl | null {
    return this.jobPostingForm.get(path) as FormControl;
  }

  private createForm(): FormGroup {
    return this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(256)]],
      description: ['', [Validators.maxLength(4000)]],
      category: [JobCategory.It],
      minHoursPerWeek: [20, [Validators.min(0), Validators.max(168)]],
      maxHoursPerWeek: [40, [Validators.min(0), Validators.max(168)]],
      allowsRemoteWork: [false],
      contactEmail: ['', [Validators.email, Validators.maxLength(256)]],
      jobUrl: ['', [Validators.maxLength(2048), Validators.pattern('https?://.+')]],
      companyId: ['', this.isNew ? [Validators.required] : []],
    });
  }

  private updateFormWithJobPosting(jobPosting: JobPostingResponse): void {
    this.jobPostingForm.patchValue({
      title: jobPosting.title || '',
      description: jobPosting.description || '',
      category: jobPosting.category || JobCategory.It,
      minHoursPerWeek: jobPosting.minHoursPerWeek || 0,
      maxHoursPerWeek: jobPosting.maxHoursPerWeek || 0,
      allowsRemoteWork: jobPosting.allowsRemoteWork || false,
      contactEmail: jobPosting.contactEmail || '',
      jobUrl: jobPosting.jobUrl || '',
      companyId: jobPosting.companyId || '',
    });

    this.jobPostingForm.markAsPristine();
    this.jobPostingForm.markAsUntouched();
  }
}
