import { HttpClient } from '@angular/common/http';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { AgendaService } from '../../agenda-service/agenda.service';
import { Contact } from '../../entities/contact';
import { Enumeration } from '../../entities/enumeration';
import { Phone } from '../../entities/phone';
import { PhoneType } from '../../enums/phone-type';
import { BaseError } from '../../http-service/base-error';
import { HttpBaseService } from '../../http-service/http-base.service';
import { apiErrorHandler } from '../../utils/api-error-handler';
import { ActivatedRoute } from '@angular/router';
import { User } from '../../entities/user';
import { AgendaAdminService } from '../../agenda-admin-service/agenda-admin.service';
import { UserService } from '../../user-service/user.service';

@Component({
  selector: 'app-contact-form',
  templateUrl: './contact-form.component.html',
  styleUrls: ['./contact-form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ContactFormComponent implements OnInit {

  form!: FormGroup;
  types!: Enumeration[];
  contact!: Contact;
  id?: any;
  users!: User[];
  isLoading = false;
  isAdmin = false;
  service!: HttpBaseService<any>;

  get phonesFieldArray(): FormArray {
    return this.form.get('phones') as FormArray;
  }

  constructor(
    private formBuilder: FormBuilder,
    private agendaService: AgendaService,
    private agendaAdminService: AgendaAdminService,
    private userService: UserService,
    private router: Router,
    protected http: HttpClient,
    private cdRef: ChangeDetectorRef,
    private snackBar: MatSnackBar,
    private activatedroute:ActivatedRoute

  ) {
    this.form = this.formBuilder.group({
      id: [null],
      name: [null, [Validators.required]],
      phones: this.formBuilder.array([]),
    })
  }

  async ngOnInit(): Promise<void> {
    this.activatedroute.paramMap.subscribe(params => {
      this.id = params.get('id');
    });
    await this.IsAdminAsync();
    await this.getPhoneTypesAsync();
    await this.getDataAsync();
  }

  async getDataAsync() {
    try {
        this.service.getByIdAsync(this.id).subscribe(contact => {
        this.form.get('id')?.setValue(contact.id);
        this.form.get('name')?.setValue(contact.name);
        contact.phones.forEach((x: Phone) => this.addPhoneForm(x));
        if (this.isAdmin) {
          this.form.get('userId')?.setValue(contact.userId);
        }
        this.cdRef.detectChanges();
      })

    } catch ({ error }) {
      apiErrorHandler(this.snackBar, error as BaseError)
    }
  }

  async IsAdminAsync(): Promise<void> {
    this.isAdmin = RegExp(`\/admin\/agenda`, 'gi').test(this.router.url);
    if (this.isAdmin) {
      this.form.addControl('userId', new FormControl(null, [Validators.required]));
      this.cdRef.detectChanges();
      await this.getAllUsersAsync();
    }
    this.service = this.isAdmin ? this.agendaAdminService : this.agendaService;
  }

  addPhoneForm(data?: Phone): void {
    this.phonesFieldArray.push(
      this.formBuilder.group({
        formattedNumber: [data?.formattedNumber, [Validators.required, this.phoneValidator]],
        description: [data?.description, [Validators.required]],
        phoneTypeId: [data?.phoneTypeId, [Validators.required]],
        PhoneType: [data?.phoneType]
      })
    )
  }

  async getAllUsersAsync(): Promise<void> {
    this.users = await lastValueFrom(this.userService.getAllUsersAsync());
  }

  removePhoneForm(index: number): void {
    this.phonesFieldArray.removeAt(index);
  }

  getMaskPhone(index: number): string {
    return this.phonesFieldArray.at(index).get("phoneTypeId")?.value === PhoneType.Cellphone
      ? '(00) 00000-0000'
      : '(00) 0000-0000'
  }

  phoneValidator(control: AbstractControl): ValidationErrors | null {
    const isValid = new RegExp(/^\([1-9]{2}\) (?:[2-8]|9[1-9])[0-9]{3}\-[0-9]{4}/).test(control.value);
    if (isValid) {
      return null;
    }
    return { formattedNumber: { value: control.value } }
  }

  async getPhoneTypesAsync(): Promise<void> {
    this.types = await lastValueFrom(this.agendaService.getPhoneTypes());
  }

  isFormValid(): boolean {
    const valid = this.form.valid;
    if (!valid) {
      this.form.markAllAsTouched();
      this.snackBar.open('There are invalid fields in the form!', undefined, { duration: 3000 });
    }
    return valid;
  }

  async saveContactAsync(): Promise<void> {
    try {
      this.isLoading = true;
      if (this.isFormValid()) {
        const data = this.form.value;
        data.id ?
          await lastValueFrom(this.service.updateAsync(data, data.id)) :
          await lastValueFrom(this.service.createAsync(data));
        this.snackBar.open('Contact saved!', undefined, { duration: 3000 });
        if(this.isAdmin) {
          this.router.navigate(['/dashboard/admin/agenda/']);
        }
        else {
          this.router.navigate(['/dashboard/agenda/']);
        }
      }
    } catch ({ error }) {
      apiErrorHandler(this.snackBar, error as BaseError);
    } finally {
      this.isLoading = false;
    }
  }
}
