import { computed, effect, inject, Injectable, signal } from '@angular/core';
import {
  JobPostingResponse,
  CreateJobPostingRequest,
  UpdateJobPostingRequest,
} from '../../../core/api';
import { JobPostingService } from '../../../core/api';
import { ActivatedRoute, Router } from '@angular/router';
import { JobCategory } from '../../../core/api';

@Injectable()
export class JobPostingDetailFacade {
  private api = inject(JobPostingService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  private jobPostingId = signal<string | null>(null);
  private jobPostingData = signal<JobPostingResponse | null>(null);
  loading = signal<boolean>(false);
  error = signal<boolean>(false);
  notFound = signal<boolean>(false);
  deleting = signal<boolean>(false);
  saving = signal<boolean>(false);
  isEdit = signal<boolean>(false);
  isNew = signal<boolean>(false);

  jobPosting = computed(() => this.jobPostingData());

  private reloadTrigger = signal<number>(0);

  constructor() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('id');
      const isNew = this.route.snapshot.data['isNew'] === true;

      this.isNew.set(isNew);

      if (isNew) {
        this.jobPostingData.set({
          title: '',
          description: '',
          category: JobCategory.It,
          minHoursPerWeek: 20,
          maxHoursPerWeek: 40,
          allowsRemoteWork: false,
          contactEmail: '',
          jobUrl: '',
          companyId: this.route.snapshot.queryParams['companyId'] || '',
          companyName: '',
          createdAt: new Date().toISOString(),
        });
        this.isEdit.set(true);
      } else if (id) {
        this.jobPostingId.set(id);
      }
    });

    effect(() => {
      const id = this.jobPostingId();
      this.reloadTrigger();

      if (this.isNew()) return;
      if (!id) return;

      this.loading.set(true);
      this.error.set(false);
      this.notFound.set(false);

      this.api.getJobPostingById(id).subscribe({
        next: (response) => {
          this.jobPostingData.set(response);
          this.loading.set(false);
        },
        error: (err) => {
          this.loading.set(false);

          if (err.status === 404) {
            this.notFound.set(true);
          } else {
            this.error.set(true);
          }
        },
      });
    });
  }

  retry(): void {
    this.reloadTrigger.set(this.reloadTrigger() + 1);
  }

  deleteJobPosting(): void {
    const id = this.jobPostingId();
    if (!id) return;

    this.deleting.set(true);

    this.api.deleteJobPosting(id).subscribe({
      next: () => {
        this.deleting.set(false);
        this.router.navigate(['/jobs']);
      },
      error: () => {
        this.deleting.set(false);
        this.error.set(true);
      },
    });
  }

  startEdit(): void {
    this.isEdit.set(true);
  }

  cancelEdit(): void {
    this.isEdit.set(false);

    if (this.isNew()) {
      this.router.navigate(['/jobs']);
    } else {
      this.retry();
    }
  }

  saveJobPosting(data: UpdateJobPostingRequest | CreateJobPostingRequest): void {
    this.saving.set(true);

    if (this.isNew()) {
      const createData = data as CreateJobPostingRequest;

      if (createData.companyId) {
        this.api.createJobPosting(createData).subscribe({
          next: (response) => {
            this.saving.set(false);
            this.isEdit.set(false);
            this.isNew.set(false);
            this.jobPostingData.set(response);
            this.jobPostingId.set(response.id!);
            this.router.navigate(['/jobs', response.id]);
          },
          error: () => {
            this.saving.set(false);
            this.error.set(true);
          },
        });
      } else {
        this.saving.set(false);
        this.error.set(true);
      }
    } else {
      const id = this.jobPostingId();
      if (!id) return;

      this.api.updateJobPosting(id, data as UpdateJobPostingRequest).subscribe({
        next: (response) => {
          this.saving.set(false);
          this.isEdit.set(false);
          this.jobPostingData.set(response);
        },
        error: () => {
          this.saving.set(false);
          this.error.set(true);
        },
      });
    }
  }
}
