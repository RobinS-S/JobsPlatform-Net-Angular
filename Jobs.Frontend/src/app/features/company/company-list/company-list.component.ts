import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { DataViewModule } from 'primeng/dataview';
import { PaginatorModule } from 'primeng/paginator';
import { InputTextModule } from 'primeng/inputtext';
import { CommonModule, NgFor, NgIf } from '@angular/common';
import { CompanyResponse } from '../../../core/api';
import { TranslateModule } from '@ngx-translate/core';
import { CountryByIsoTwoPipe } from '../../../core/pipes/country-from-iso-two.pipe';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CompanyListFacade } from './company-list.facade';

@Component({
  selector: 'app-company-list',
  imports: [
    CommonModule,
    DataViewModule,
    PaginatorModule,
    InputTextModule,
    NgFor,
    NgIf,
    TranslateModule,
    CountryByIsoTwoPipe,
    ProgressSpinnerModule,
    ReactiveFormsModule,
    RouterModule,
  ],
  providers: [CompanyListFacade],
  templateUrl: './company-list.component.html',
  styleUrl: './company-list.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CompanyListComponent {
  facade = inject(CompanyListFacade);
  trackById = (_: number, item: CompanyResponse) => item.id;

  filteringOptions = new FormGroup({
    searchControl: new FormControl('', [Validators.maxLength(100)]),
    minActiveJobsControl: new FormControl(1, [Validators.min(0)]),
  });

  constructor() {
    this.filteringOptions.controls.searchControl.valueChanges.subscribe((value) => {
      if (value !== null && value !== undefined) {
        this.facade.setSearch(value);
      }
    });

    this.filteringOptions.controls.minActiveJobsControl.valueChanges.subscribe((value) => {
      if (value !== null && value !== undefined) {
        this.facade.setMinActiveJobs(value);
      }
    });
  }
}
