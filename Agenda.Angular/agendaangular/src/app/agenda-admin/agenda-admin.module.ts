import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgendaAdminComponent } from './agenda-admin.component';
import { MaterialModule } from '../shared/material/material.module';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ConfirmModalDialogModule } from '../shared/components/confirm-modal-dialog/confirm-modal-dialog.module';
import { SearchingModule } from '../shared/components/searching/searching.module';

@NgModule({
  declarations: [
    AgendaAdminComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    HttpClientModule,
    RouterModule,
    ConfirmModalDialogModule,
    SearchingModule
  ]
})
export class AgendaAdminModule { }
