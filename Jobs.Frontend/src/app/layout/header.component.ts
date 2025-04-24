import { Component, inject } from '@angular/core';
import { RouterLink, Router, RouterLinkActive } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  imports: [RouterLink, RouterLinkActive, TranslateModule],
})
export class HeaderComponent {
  private translate = inject(TranslateService);

  constructor(public router: Router) {}
}
