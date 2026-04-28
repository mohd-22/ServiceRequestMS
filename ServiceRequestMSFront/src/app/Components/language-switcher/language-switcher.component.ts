import { Component, OnInit } from '@angular/core';
import { LanguageService } from '../../Services/language.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-language-switcher',
  templateUrl: './language-switcher.component.html',
  styleUrls: ['./language-switcher.component.css']
})
export class LanguageSwitcherComponent implements OnInit {
  currentLanguage: string = 'en';
  isRTL: boolean = false;

  constructor(
    private languageService: LanguageService,
    private translate: TranslateService
  ) { }

  ngOnInit(): void {
    this.languageService.currentLanguage$.subscribe(lang => {
      this.currentLanguage = lang;
    });

    this.languageService.isRTL$.subscribe(isRTL => {
      this.isRTL = isRTL;
    });
  }

  toggleLanguage(): void {
    this.languageService.toggleLanguage();
  }

  getLanguageLabel(): string {
    return this.currentLanguage === 'en' ? 'العربية' : 'English';
  }
}
