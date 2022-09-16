import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { Enumeration } from '../shared/entities/enumeration';
import { User } from '../shared/entities/user';
import { BaseError } from '../shared/http-service/base-error';
import { RegisterService } from '../shared/register-service/register.service';
import { UserService } from '../shared/user-service/user.service';
import { apiErrorHandler } from '../shared/utils/api-error-handler';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RegisterComponent {

  form!: FormGroup;
  roles!: Enumeration[];
  constructor(
    private registerService: RegisterService,
    private formBuilder: FormBuilder,
    private router: Router,
    private snackBar: MatSnackBar,
  ) {
    this.form = this.formBuilder.group({
      name: [null, [Validators.required]],
      username: [null, [Validators.required]],
      password: [null, [Validators.required]],
      email: [null, [Validators.required, Validators.email]],
      userRoleId: [2],
    })
  }

  async saveUserAsync(): Promise<void> {
    try {
      if (this.isFormValid()) {
        const data = this.form.value as User;
        await lastValueFrom(this.registerService.registerAsync(data));
        this.snackBar.open('User saved!', undefined, { duration: 3000 });
        this.router.navigate(['/dashboard/login/']);
      }
    } catch ({ error }) {
      apiErrorHandler(this.snackBar, error as BaseError);
    } finally {
    }
  }

  isFormValid(): boolean {
    const valid = this.form.valid;
    if (!valid) {
      this.form.markAllAsTouched();
      this.snackBar.open('There are invalid fields in the form!', undefined, { duration: 3000 });
    }
    return valid;
  }
}
