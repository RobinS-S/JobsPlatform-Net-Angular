import { computed, effect, inject, Injectable, signal } from '@angular/core';
import { CompanyResponsePaginatedListResult } from '../../../core/api';
import { CompanyService } from '../../../core/api';
import { DataViewPageEvent } from 'primeng/dataview';

@Injectable()
export class CompanyListFacade {
  private api = inject(CompanyService);

  pageNumber = signal<number>(1);
  pageSize = signal<number>(15);
  searchText = signal<string>('');
  minActiveJobs = signal<number>(1);
  rowsPerPage = [10, 15, 20, 25, 50, 100];

  private result = signal<CompanyResponsePaginatedListResult>({
    items: [],
    totalCount: 0,
  });

  loading = signal<boolean>(false);
  error = signal<boolean>(false);
  private reloadTrigger = signal<number>(0);

  constructor() {
    effect(() => {
      this.reloadTrigger();
      const search = this.searchText();
      const safeSearchText = search.length > 100 ? search.slice(0, 100) : search;

      this.loading.set(true);
      this.error.set(false);

      this.api
        .getAllCompanies(this.pageNumber(), this.pageSize(), safeSearchText, this.minActiveJobs())
        .subscribe({
          next: (r) => {
            this.result.set(r);
            this.loading.set(false);
          },
          error: () => {
            this.error.set(true);
            this.loading.set(false);
          },
        });
    });
  }

  items = computed(() => this.result().items ?? []);
  totalRecords = computed(() => this.result().totalCount ?? 0);

  setSearch(txt: string) {
    this.searchText.set(txt);
    this.pageNumber.set(1);
  }

  setMinActiveJobs(value: number) {
    this.minActiveJobs.set(value);
    this.pageNumber.set(1);
  }

  onPageChange(event: DataViewPageEvent) {
    const newPage = event.first / event.rows + 1;

    this.pageSize.set(event.rows);
    this.pageNumber.set(newPage);
  }

  retry() {
    this.reloadTrigger.set(this.reloadTrigger() + 1);
  }
}
