import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { CountryByIsoTwoPipe } from '../../../core/pipes/country-from-iso-two.pipe';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { RouterModule } from '@angular/router';
import { CompanyDetailFacade } from './company-detail.facade';
import { SafePipe } from '../../../core/pipes/safe.pipe';
import { ConfirmationService } from 'primeng/api';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { CreateCompanyRequest, UpdateCompanyRequest } from '../../../core/api';
import { CompanyFormComponent } from './company-form/company-form.component';

@Component({
  selector: 'app-company-detail',
  standalone: true,
  imports: [
    CommonModule,
    TranslateModule,
    CountryByIsoTwoPipe,
    ProgressSpinnerModule,
    RouterModule,
    SafePipe,
    ConfirmDialogModule,
    CompanyFormComponent,
  ],
  providers: [CompanyDetailFacade, ConfirmationService],
  templateUrl: './company-detail.component.html',
  styleUrl: './company-detail.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CompanyDetailComponent {
  facade = inject(CompanyDetailFacade);
  private confirmationService = inject(ConfirmationService);
  private translateService = inject(TranslateService);

  confirmDelete(): void {
    this.confirmationService.confirm({
      message: this.translateService.instant('generic.confirmDeleteMessage'),
      header: this.translateService.instant('generic.confirmDelete'),
      accept: () => {
        this.facade.deleteCompany();
      },
    });
  }

  onSaveCompany(formData: UpdateCompanyRequest | CreateCompanyRequest): void {
    this.facade.saveCompany(formData);
  }

  onCancelEdit(): void {
    this.facade.cancelEdit();
  }
}
