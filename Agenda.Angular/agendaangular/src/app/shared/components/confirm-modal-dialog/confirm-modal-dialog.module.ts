import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConfirmModalDialogComponent } from './confirm-modal-dialog.component';
import { MaterialModule } from '../../material/material.module';



@NgModule({
  declarations: [
    ConfirmModalDialogComponent
  ],
  imports: [
    CommonModule,
    MaterialModule
  ]
})
export class ConfirmModalDialogModule { }
