import { Injectable } from '@angular/core';
import { AbstractControl } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root',
})
export class ValidationService {
  constructor(private translate: TranslateService) {}

  getValidationMessage(control: AbstractControl | null, fieldName: string): string | null {
    if (!control || control.valid) {
      return null;
    }

    const errors = control.errors;
    if (!errors) return null;

    if (errors['required']) {
      return this.translate.instant('validation.required', { field: fieldName });
    }

    if (errors['maxlength']) {
      const maxLength = errors['maxlength'].requiredLength;
      return this.translate.instant('validation.maxLength', { field: fieldName, maxLength });
    }

    if (errors['pattern']) {
      return this.translate.instant('validation.pattern', { field: fieldName });
    }

    if (errors['email']) {
      return this.translate.instant('validation.email');
    }

    if (errors['url']) {
      return this.translate.instant('validation.url');
    }

    return this.translate.instant('validation.invalid', { field: fieldName });
  }

  hasError(control: AbstractControl | null): boolean {
    if (!control) return false;
    return control.invalid && (control.dirty || control.touched);
  }
}
