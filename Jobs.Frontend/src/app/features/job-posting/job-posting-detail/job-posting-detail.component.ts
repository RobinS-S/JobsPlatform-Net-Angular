import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { RouterModule } from '@angular/router';
import { ConfirmationService } from 'primeng/api';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { CreateJobPostingRequest, UpdateJobPostingRequest } from '../../../core/api';
import { JobPostingFormComponent } from './job-posting-form/job-posting-form.component';
import { JobPostingDetailFacade } from './job-posting-detail.facade';

@Component({
  selector: 'app-job-posting-detail',
  standalone: true,
  imports: [
    CommonModule,
    TranslateModule,
    ProgressSpinnerModule,
    RouterModule,
    ConfirmDialogModule,
    JobPostingFormComponent,
  ],
  providers: [JobPostingDetailFacade, ConfirmationService],
  templateUrl: './job-posting-detail.component.html',
  styleUrl: './job-posting-detail.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class JobPostingDetailComponent {
  facade = inject(JobPostingDetailFacade);
  private confirmationService = inject(ConfirmationService);
  private translateService = inject(TranslateService);

  confirmDelete(): void {
    this.confirmationService.confirm({
      message: this.translateService.instant('generic.confirmDeleteMessage'),
      header: this.translateService.instant('generic.confirmDelete'),
      accept: () => {
        this.facade.deleteJobPosting();
      },
    });
  }

  onSaveJobPosting(formData: UpdateJobPostingRequest | CreateJobPostingRequest): void {
    this.facade.saveJobPosting(formData);
  }

  onCancelEdit(): void {
    this.facade.cancelEdit();
  }
}
