import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { ConfirmModalDialogConfig } from '../shared/components/confirm-modal-dialog/confirm-modal-dialog-config';
import { ConfirmModalDialogService } from '../shared/components/confirm-modal-dialog/confirm-modal-dialog.service';
import { SearchingForm } from '../shared/components/searching/searching-form';
import { SearchingInputConfig } from '../shared/components/searching/searching-input-config';
import { User } from '../shared/entities/user';
import { BaseError } from '../shared/http-service/base-error';
import { BaseParams } from '../shared/http-service/base-params';
import { PaginationResponse } from '../shared/http-service/pagination-response';
import { UserService } from '../shared/user-service/user.service';
import { apiErrorHandler } from '../shared/utils/api-error-handler';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UsersComponent implements OnInit {

  data!: PaginationResponse<User>;
  totalPages!: number;
  searchingConfig =  {
    searchAction: (query) => this.searchAsync(query),
    params: [
      { label: "Name", name: "name" },
      { label: "Username", name: "username" },
      { label: "Email", name: "email" },
    ]
  } as SearchingInputConfig;

  constructor(
    private userService: UserService,
    private snackBar: MatSnackBar,
    private confirmModalDialogService: ConfirmModalDialogService,
    private cdRef: ChangeDetectorRef,
    private router: Router
  ) { }

  async ngOnInit(): Promise<void> {
    await this.refreshTableAsync();
  }

  async getDataAsync(params = new BaseParams()): Promise<void> {
    try {
      this.data = await lastValueFrom(this.userService.getAsync(params));
      this.totalPages = this.data.total;
    } catch ({ error }) {
      apiErrorHandler(this.snackBar, error as BaseError)
    }
  }

  onAdd() {
    this.router.navigate(['/dashboard/admin/users/form/0'])
  }

  onEdit(id?: number) {
    this.router.navigate(['/dashboard/admin/users/form', id])
  }

  async deleteUserAsync(id: number): Promise<void> {
    const config = {
      title: 'Confirm exclusion',
      message: 'Do you really wish to delete this user?',
    } as ConfirmModalDialogConfig;
    this.confirmModalDialogService.open(config);
    this.confirmModalDialogService.closed.subscribe(async (result) => {
      if (result) {
        await lastValueFrom(this.userService.deleteAsync(id));
        await this.refreshTableAsync();
      }
    });
  }

  async refreshTableAsync(): Promise<void> {
    await this.getDataAsync();
    this.cdRef.detectChanges();
  }

  async changePageAsync(event: PageEvent): Promise<void> {
    const params = {
      take: event.pageSize,
      skip: event.pageIndex * event.pageSize,
    } as BaseParams;
    await this.getDataAsync(params);
  }

  async searchAsync(query: SearchingForm): Promise<void> {
    const params = {
      [query.field]: query.value
    } as BaseParams;
    await this.getDataAsync(params);
    this.cdRef.detectChanges();
  }
}
