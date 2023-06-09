import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent {
  sideMenuOpen: boolean = true;

  toggleMenu(button: HTMLButtonElement) {
    button.classList.toggle('open');
    this.sideMenuOpen = !this.sideMenuOpen;
  }
}
