import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { LoadingModalConfig } from '../shared/components/loading-modal/loading-modal-config';
import { LoadingModalService } from '../shared/components/loading-modal/loading-modal.service';
import { AuthUser } from '../shared/entities/auth-user';
import { AuthService } from '../shared/http-service/auth/auth.service';
import { BaseError } from '../shared/http-service/base-error';
import { apiErrorHandler } from '../shared/utils/api-error-handler';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent{
  form!: FormGroup;
  isLoading = false;

  constructor(
    private formBuilder: FormBuilder,
    private snackBar: MatSnackBar,
    private authService: AuthService,
    private router: Router,
    private loadingModalService: LoadingModalService,
  ) {
    this.form = this.formBuilder.group({
      username: [null, [Validators.required]],
      password: [null, [Validators.required]],
    })
   }

   isFormValid(): boolean {
    const valid = this.form.valid;
    if (!valid) {
      this.form.markAllAsTouched();
      this.snackBar.open("Há campos inválidos no formulário!", undefined, { duration: 3000 })
    }
    return valid;
  }

  async loginAsync(): Promise<void> {
    try {
      this.isLoading = true;
      await this.loadingModal();
      if (this.isFormValid()) {
        const data = this.form.value as AuthUser;
        const { token } = await lastValueFrom(this.authService.loginAsync(data));
        this.authService.setToken(token);
        this.router.navigate(['dashboard', 'home'])
      }
    } catch ({error}) {
      apiErrorHandler(this.snackBar, error as BaseError);
    }finally {
      this.isLoading = false;
      await this.loadingModal();
    }
  }

  async loadingModal(): Promise<void> {
    if(this.isLoading) {
      const config = {
        message: 'Loading...',
      } as LoadingModalConfig;
      this.loadingModalService.open(config);
    }else {
      this.loadingModalService.close();
    }
  }
}
