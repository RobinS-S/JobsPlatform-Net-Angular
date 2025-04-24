import { Injectable } from '@angular/core';
import { LangChangeEvent, TranslateService } from '@ngx-translate/core';
import * as countries from 'i18n-iso-countries';
import type { Country } from '../models/country';

import en from 'i18n-iso-countries/langs/en.json';

@Injectable({ providedIn: 'root' })
export class CountryService {
  private iso = countries;
  private loaded = new Set<string>();
  private currentLang = 'en';
  private locales: Record<string, any> = { en };

  constructor(translate: TranslateService) {
    this.currentLang = translate.getBrowserLang() || translate.getDefaultLang() || 'en';

    translate.onLangChange.subscribe((e: LangChangeEvent) => {
      this.currentLang = e.lang;
    });
  }

  private loadLocale(lang: string) {
    if (!this.loaded.has(lang)) {
      const locale = this.locales[lang] ?? this.locales['en'];

      this.iso.registerLocale(locale);
      this.loaded.add(lang);
    }
  }

  async getAll(): Promise<Country[]> {
    this.loadLocale(this.currentLang);

    const names = this.iso.getNames(this.currentLang, { select: 'official' });

    return Object.entries(names)
      .map(([code, name]) => ({ code, name }))
      .sort((a, b) => a.name.localeCompare(b.name));
  }

  async getName(code: string): Promise<string> {
    this.loadLocale(this.currentLang);

    return this.iso.getName(code, this.currentLang, { select: 'official' }) ?? code;
  }
}
