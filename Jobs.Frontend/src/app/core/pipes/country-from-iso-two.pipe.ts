import { Pipe, PipeTransform } from '@angular/core';
import { CountryService } from '../services/country.service';

@Pipe({
  name: 'countryFromIsoTwo',
  standalone: true,
  pure: true,
})
export class CountryByIsoTwoPipe implements PipeTransform {
  constructor(private svc: CountryService) {}

  transform(code: string | undefined | null): Promise<string> {
    if (!code) {
      return Promise.resolve('');
    }

    return this.svc.getName(code);
  }
}
