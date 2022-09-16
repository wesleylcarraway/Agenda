import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersComponent } from './users.component';
import { MaterialModule } from '../shared/material/material.module';
import { UsersFormComponent } from './users-form/users-form.component';
import { SearchingModule } from '../shared/components/searching/searching.module';

@NgModule({
  declarations: [
    UsersComponent,
    UsersFormComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    SearchingModule
  ]
})
export class UsersModule { }
