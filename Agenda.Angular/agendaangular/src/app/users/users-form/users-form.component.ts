import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { Enumeration } from 'src/app/shared/entities/enumeration';
import { User } from 'src/app/shared/entities/user';
import { BaseError } from 'src/app/shared/http-service/base-error';
import { UserService } from 'src/app/shared/user-service/user.service';
import { apiErrorHandler } from 'src/app/shared/utils/api-error-handler';

@Component({
  selector: 'app-users-form',
  templateUrl: './users-form.component.html',
  styleUrls: ['./users-form.component.scss']
})
export class UsersFormComponent implements OnInit {

  form!: FormGroup;
  roles!: Enumeration[];
  isLoading = false;
  id?: any;

  constructor(
    private formBuilder: FormBuilder,
    private usersService: UserService,
    private router: Router,
    protected http: HttpClient,
    private cdRef: ChangeDetectorRef,
    private snackBar: MatSnackBar,
    private activatedroute: ActivatedRoute

  ) {
    this.form = this.formBuilder.group({
      id: [null],
      name: [null, [Validators.required]],
      username: [null, [Validators.required]],
      password: [null, [Validators.required]],
      email: [null, [Validators.required, Validators.email]],
      userRoleId: [null, [Validators.required]],
    })
   }

   async ngOnInit(): Promise<void> {
    await this.getUserRoles();
    this.activatedroute.paramMap.subscribe(params => {
      this.id = params.get('id');
    });

    await this.getDataAsync();
  }

  async getDataAsync() {
    try {
        this.usersService.getByIdAsync(this.id).subscribe(user => {
        this.form.get('id')?.setValue(user.id);
        this.form.get('name')?.setValue(user.name);
        this.form.get('username')?.setValue(user.username);
        this.form.get('email')?.setValue(user.email);
        this.form.get('password')?.setValue(user.password);
        this.form.get('userRoleId')?.setValue(user.userRoleId);

        this.cdRef.detectChanges();
      })
    } catch ({ error }) {
      apiErrorHandler(this.snackBar, error as BaseError)
    }
  }

  async getUserRoles(): Promise<void> {
    this.roles = await lastValueFrom(this.usersService.getUserRoles());
  }

  async saveUserAsync(): Promise<void> {
    try {
      this.isLoading = true;
      if (this.isFormValid()) {
        const data = this.form.value as User;
        data.id ?
          await lastValueFrom(this.usersService.updateAsync(data, data.id)) :
          await lastValueFrom(this.usersService.createAsync(data));
        this.snackBar.open('User saved!', undefined, { duration: 3000 });
        this.router.navigate(['/dashboard/admin/users/']);
      }
    } catch ({ error }) {
      apiErrorHandler(this.snackBar, error as BaseError);
    } finally {
      this.isLoading = false;
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
