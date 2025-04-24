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
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { SelectModule } from 'primeng/select';
import { CompanyResponse, CreateCompanyRequest, UpdateCompanyRequest } from '../../../../core/api';
import { Country } from '../../../../core/models/country';
import { CountryService } from '../../../../core/services/country.service';
import { ValidationService } from '../../../../core/services/validation.service';
import { allNonEmpty } from '../../../../core/validators/all-non-empty.validator';

@Component({
  selector: 'app-company-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, TranslateModule, SelectModule],
  templateUrl: './company-form.component.html',
  styleUrl: './company-form.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CompanyFormComponent implements OnInit {
  @Input() company: CompanyResponse | null = null;
  @Input() saving = false;
  @Input() isNew = false;

  @Output() companySaved = new EventEmitter<UpdateCompanyRequest | CreateCompanyRequest>();
  @Output() cancel = new EventEmitter<void>();

  private fb = inject(FormBuilder);
  private countryService = inject(CountryService);
  private validationService = inject(ValidationService);
  private translateService = inject(TranslateService);

  companyForm: FormGroup;
  countries = signal<Country[]>([]);

  constructor() {
    this.companyForm = this.createForm();
    this.loadCountries();
  }

  ngOnInit(): void {
    if (this.company) {
      this.updateFormWithCompany(this.company);
    }
  }

  onSubmit(): void {
    if (this.companyForm.invalid) {
      this.companyForm.markAllAsTouched();
      return;
    }

    const formValue = this.companyForm.getRawValue();
    this.companySaved.emit(formValue);
  }

  onCancel(): void {
    this.cancel.emit();
  }

  get addressLines(): FormArray {
    return this.companyForm.get('location.addressLines') as FormArray;
  }

  addAddressLine(): void {
    this.addressLines.push(this.fb.control(''));
  }

  removeAddressLine(index: number): void {
    this.addressLines.removeAt(index);
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

  private async loadCountries(): Promise<void> {
    try {
      const countries = await this.countryService.getAll();
      this.countries.set(countries);
    } catch (error) {
      console.error('Error loading countries:', error);
    }
  }

  private getControl(path: string): FormControl | null {
    return this.companyForm.get(path) as FormControl;
  }

  private createForm(): FormGroup {
    return this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(256)]],
      website: ['', [Validators.maxLength(2048), Validators.pattern('https?://.+')]],
      location: this.fb.group({
        addressLines: this.fb.array(
          [],
          [Validators.minLength(1), Validators.maxLength(4), allNonEmpty()]
        ),
        city: ['', [Validators.required]],
        stateProvince: [''],
        postalCode: [''],
        countryCode: ['us', [Validators.required]],
      }),
    });
  }

  private updateFormWithCompany(company: CompanyResponse): void {
    this.addressLines.clear();

    if (company.location?.addressLines && Array.isArray(company.location.addressLines)) {
      for (const line of company.location.addressLines) {
        this.addressLines.push(this.fb.control(line));
      }
    }

    if (this.addressLines.length === 0) {
      this.addAddressLine();
    }

    this.companyForm.patchValue({
      name: company.name || '',
      website: company.website || '',
      location: {
        city: company.location?.city || '',
        stateProvince: company.location?.stateProvince || '',
        postalCode: company.location?.postalCode || '',
        countryCode: company.location?.countryCode || 'us',
      },
    });

    this.companyForm.markAsPristine();
    this.companyForm.markAsUntouched();
  }
}
