import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgendaComponent } from './agenda.component';
import { MaterialModule } from '../shared/material/material.module';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ConfirmModalDialogModule } from '../shared/components/confirm-modal-dialog/confirm-modal-dialog.module';
import { ContactFormModule } from '../shared/components/contact-form/contact-form.module';
import { SearchingModule } from '../shared/components/searching/searching.module';
import { LoadingModalModule } from '../shared/components/loading-modal/loading-modal.module';

@NgModule({
  declarations: [
    AgendaComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    HttpClientModule,
    RouterModule,
    MatSnackBarModule,
    ConfirmModalDialogModule,
    ContactFormModule,
    SearchingModule,
    LoadingModalModule
  ],
  exports: [
    AgendaComponent
  ]
})
export class AgendaModule { }
