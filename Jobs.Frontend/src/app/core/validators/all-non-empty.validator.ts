import {
  AbstractControl,
  ValidationErrors,
  ValidatorFn,
  FormArray,
  FormControl,
} from '@angular/forms';

export const allNonEmpty =
  (): ValidatorFn =>
  (control: AbstractControl): ValidationErrors | null => {
    const array = control as FormArray<FormControl<string | null>>;
    return array.controls.every((c) => (c.value ?? '').trim().length > 0)
      ? null
      : { emptyLine: true };
  };
