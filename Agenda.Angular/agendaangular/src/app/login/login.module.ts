import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login.component';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MatInputModule } from '@angular/material/input';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { HttpClientModule } from '@angular/common/http';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MaterialModule } from '../shared/material/material.module';
import { LoadingModalModule } from '../shared/components/loading-modal/loading-modal.module';

@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    MatProgressBarModule,
    ReactiveFormsModule,
    RouterModule,
    MatInputModule,
    FlexLayoutModule,
    MatSnackBarModule,
    MatFormFieldModule,
    HttpClientModule,
    LoadingModalModule
  ],
  exports: [
    LoginComponent
  ]
})
export class LoginModule { }
