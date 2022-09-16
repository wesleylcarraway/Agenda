import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LayoutComponent } from './layout.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { NavbarModule } from '../navbar/navbar.module';
import { HomeModule } from 'src/app/home/home.module';
import { AgendaModule } from 'src/app/agenda/agenda.module';
import { AgendaAdminModule } from 'src/app/agenda-admin/agenda-admin.module';
import { UsersModule } from 'src/app/users/users.module';

@NgModule({
  declarations: [
    LayoutComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    FlexLayoutModule,
    NavbarModule,
    HomeModule,
    AgendaModule,
    AgendaAdminModule,
    UsersModule
  ],
  exports: [
    LayoutComponent
  ]
})
export class LayoutModule { }
