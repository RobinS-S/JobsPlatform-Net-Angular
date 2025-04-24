import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./features/home/home.component').then((m) => m.HomeComponent),
  },
  {
    path: 'companies',
    loadComponent: () =>
      import('./features/company/company-list/company-list.component').then(
        (m) => m.CompanyListComponent
      ),
  },
  {
    path: 'companies/new',
    loadComponent: () =>
      import('./features/company/company-detail/company-detail.component').then(
        (m) => m.CompanyDetailComponent
      ),
    data: { isNew: true },
  },
  {
    path: 'companies/:id',
    loadComponent: () =>
      import('./features/company/company-detail/company-detail.component').then(
        (m) => m.CompanyDetailComponent
      ),
  },
  {
    path: 'jobs',
    loadComponent: () =>
      import('./features/job-posting/job-posting-list/job-posting-list.component').then(
        (m) => m.JobPostingListComponent
      ),
  },
  {
    path: 'jobs/new',
    loadComponent: () =>
      import('./features/job-posting/job-posting-detail/job-posting-detail.component').then(
        (m) => m.JobPostingDetailComponent
      ),
    data: { isNew: true },
  },
  {
    path: 'jobs/:id',
    loadComponent: () =>
      import('./features/job-posting/job-posting-detail/job-posting-detail.component').then(
        (m) => m.JobPostingDetailComponent
      ),
  },
];
