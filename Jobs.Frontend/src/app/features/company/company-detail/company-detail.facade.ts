import { computed, effect, inject, Injectable, signal } from '@angular/core';
import { CompanyResponse, CreateCompanyRequest, UpdateCompanyRequest } from '../../../core/api';
import { CompanyService } from '../../../core/api';
import { ActivatedRoute, Router } from '@angular/router';

@Injectable()
export class CompanyDetailFacade {
  private api = inject(CompanyService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  private companyId = signal<string | null>(null);
  private companyData = signal<CompanyResponse | null>(null);
  loading = signal<boolean>(false);
  error = signal<boolean>(false);
  notFound = signal<boolean>(false);
  deleting = signal<boolean>(false);
  saving = signal<boolean>(false);
  isEdit = signal<boolean>(false);
  isNew = signal<boolean>(false);

  company = computed(() => this.companyData());

  private reloadTrigger = signal<number>(0);

  constructor() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('id');
      const isNew = this.route.snapshot.data['isNew'] === true;

      this.isNew.set(isNew);

      if (isNew) {
        this.companyData.set({
          name: '',
          location: {
            addressLines: [],
            city: '',
            countryCode: 'us',
            postalCode: '',
          },
          website: '',
        });
        this.isEdit.set(true);
      } else if (id) {
        this.companyId.set(id);
      }
    });

    effect(() => {
      const id = this.companyId();
      this.reloadTrigger();

      if (this.isNew()) return;
      if (!id) return;

      this.loading.set(true);
      this.error.set(false);
      this.notFound.set(false);

      this.api.getCompanyById(id).subscribe({
        next: (response) => {
          this.companyData.set(response);
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

  deleteCompany(): void {
    const id = this.companyId();
    if (!id) return;

    this.deleting.set(true);

    this.api.deleteCompany(id).subscribe({
      next: () => {
        this.deleting.set(false);
        this.router.navigate(['/companies']);
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
      this.router.navigate(['/companies']);
    } else {
      this.retry();
    }
  }

  saveCompany(data: UpdateCompanyRequest | CreateCompanyRequest): void {
    this.saving.set(true);

    if (data.location?.addressLines) {
      data.location.addressLines = data.location.addressLines.filter((line) => line.trim() !== '');
    }

    if (this.isNew()) {
      this.api.createCompany(data as CreateCompanyRequest).subscribe({
        next: (response) => {
          this.saving.set(false);
          this.isEdit.set(false);
          this.isNew.set(false);
          this.companyData.set(response);
          this.companyId.set(response.id!);
          this.router.navigate(['/companies', response.id]);
        },
        error: () => {
          this.saving.set(false);
          this.error.set(true);
        },
      });
    } else {
      const id = this.companyId();
      if (!id) return;

      this.api.updateCompany(id, data as UpdateCompanyRequest).subscribe({
        next: (response) => {
          this.saving.set(false);
          this.isEdit.set(false);
          this.companyData.set(response);
        },
        error: () => {
          this.saving.set(false);
          this.error.set(true);
        },
      });
    }
  }
}
