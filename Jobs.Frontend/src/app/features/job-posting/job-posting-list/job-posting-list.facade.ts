import { computed, effect, inject, Injectable, signal } from '@angular/core';
import { JobPostingResponsePaginatedListResult, JobCategory } from '../../../core/api';
import { JobPostingService } from '../../../core/api';
import { DataViewPageEvent } from 'primeng/dataview';
import { ActivatedRoute } from '@angular/router';

@Injectable()
export class JobPostingListFacade {
  private api = inject(JobPostingService);
  private route = inject(ActivatedRoute);

  pageNumber = signal<number>(1);
  pageSize = signal<number>(15);
  searchText = signal<string>('');
  category = signal<JobCategory | null>(null);
  companyId = signal<string | null>(null);
  rowsPerPage = [10, 15, 20, 25, 50, 100];

  private result = signal<JobPostingResponsePaginatedListResult>({
    items: [],
    totalCount: 0,
  });

  loading = signal<boolean>(false);
  error = signal<boolean>(false);
  private reloadTrigger = signal<number>(0);

  constructor() {
    this.route.queryParamMap.subscribe((params) => {
      const companyId = params.get('companyId');
      if (companyId) {
        this.companyId.set(companyId);
      }
    });

    effect(() => {
      this.reloadTrigger();

      const search = this.searchText();
      const shortenedText = search.length > 100 ? search.slice(0, 100) : search;
      const categoryValue = this.category();
      const companyIdValue = this.companyId();

      this.loading.set(true);
      this.error.set(false);

      this.api
        .getAllJobPostings(
          this.pageNumber(),
          this.pageSize(),
          shortenedText,
          companyIdValue || undefined,
          categoryValue || undefined
        )
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

  setCategory(category: JobCategory | null) {
    this.category.set(category);
    this.pageNumber.set(1);
  }

  setCompanyId(companyId: string | null) {
    this.companyId.set(companyId);
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
