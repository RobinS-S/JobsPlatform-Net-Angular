import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataViewModule } from 'primeng/dataview';
import { PaginatorModule } from 'primeng/paginator';
import { InputTextModule } from 'primeng/inputtext';
import { SelectModule } from 'primeng/select';
import { TranslateModule } from '@ngx-translate/core';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { JobPostingResponse, CompanyService } from '../../../core/api';
import { JobCategory } from '../../../core/api';
import { JobPostingListFacade } from './job-posting-list.facade';
import { AutoCompleteCompleteEvent, AutoCompleteModule } from 'primeng/autocomplete';

interface CompanyOption {
  label: string;
  value: string;
}

@Component({
  selector: 'app-job-posting-list',
  standalone: true,
  imports: [
    CommonModule,
    DataViewModule,
    PaginatorModule,
    InputTextModule,
    SelectModule,
    TranslateModule,
    ProgressSpinnerModule,
    ReactiveFormsModule,
    RouterModule,
    AutoCompleteModule,
  ],
  providers: [JobPostingListFacade],
  templateUrl: './job-posting-list.component.html',
  styleUrl: './job-posting-list.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class JobPostingListComponent {
  facade = inject(JobPostingListFacade);
  private companyService = inject(CompanyService);

  trackById = (_: number, item: JobPostingResponse) => item.id;

  filteringOptions = new FormGroup({
    searchControl: new FormControl('', [Validators.maxLength(100)]),
    categoryControl: new FormControl<JobCategory | null>(null),
    companyIdControl: new FormControl<CompanyOption | null>(null),
  });

  jobCategories = Object.values(JobCategory);
  companies = signal<CompanyOption[]>([]);

  constructor() {
    this.filteringOptions.controls.searchControl.valueChanges.subscribe((value) => {
      if (value !== null && value !== undefined) {
        this.facade.setSearch(value);
      }
    });

    this.filteringOptions.controls.categoryControl.valueChanges.subscribe((value) => {
      this.facade.setCategory(value);
    });

    if (this.facade.companyId()) {
      this.loadCompanies('', this.facade.companyId() as string);
    }
  }

  searchCompanies(event: AutoCompleteCompleteEvent) {
    this.loadCompanies(event.query);
  }

  onCompanySelect(event: any) {
    if (event && event.value) {
      this.facade.setCompanyId(event.value.value);
    }
  }

  onCompanyClear() {
    this.facade.setCompanyId(null);
  }

  private async loadCompanies(searchText: string, specificCompanyId?: string): Promise<void> {
    try {
      if (specificCompanyId) {
        const response = await this.companyService.getCompanyById(specificCompanyId).toPromise();
        if (response) {
          const companyOption: CompanyOption = {
            label: response.name || '',
            value: response.id || '',
          };

          this.companies.set([companyOption]);
          this.filteringOptions.controls.companyIdControl.setValue(companyOption);
          return;
        }
      }

      const result = await this.companyService.getAllCompanies(1, 100, searchText).toPromise();
      const companyOptions = (result?.items || []).map((company) => ({
        label: company.name || '',
        value: company.id || '',
      }));
      this.companies.set(companyOptions);
    } catch (error) {
      console.error('Error loading companies', error);
    }
  }
}
