import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Roles } from '../../enums/roles';
import { AuthService } from '../../http-service/auth/auth.service';
import { NavbarItem } from './navbar-item';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  items: NavbarItem[] = [];
  themeIcon: string | null = localStorage.getItem("theme");

  constructor(
    private router: Router,
    private authService: AuthService,
  ) {
    localStorage.setItem("theme", 'nightlight')
  }

  ngOnInit(): void {
    this.setNavbarItems();
  }

  setNavbarItems(): void {
    this.items = [
      { name: 'Contacts', url: 'agenda', icon: 'people' }
    ];
    if (this.authService.getRole() == Roles.ADMIN) {
      this.items.push(
        { name: 'All Contacts', url: 'admin/agenda', icon: 'menu_book' },
        { name: 'Users', url: 'admin/users', icon: 'request_page' },
      )
    }
  }

  logout(): void {
    this.authService.clearToken();
    this.router.navigate(['login'])
    this.themeIcon = 'nightlight'
  }

  switchTheme(): string {
    const theme = document.body.classList.toggle('dark-theme')
    if(theme) {
      this.themeIcon = 'sunny'
    }
    else {
      this.themeIcon = 'nightlight'
    }

    if(this.themeIcon != null)
      localStorage.setItem("theme", this.themeIcon)

    return this.themeIcon;
  }
}
