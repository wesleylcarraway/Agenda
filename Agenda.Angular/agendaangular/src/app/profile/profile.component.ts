import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { ConfirmModalDialogConfig } from '../shared/components/confirm-modal-dialog/confirm-modal-dialog-config';
import { ConfirmModalDialogService } from '../shared/components/confirm-modal-dialog/confirm-modal-dialog.service';
import { User } from '../shared/entities/user';
import { AuthService } from '../shared/http-service/auth/auth.service';
import { UserService } from '../shared/user-service/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  user!: User;

  constructor(
    private userService: UserService,
    private router: Router,
    private confirmModalService: ConfirmModalDialogService,
    private authService: AuthService
  ) { }

  async ngOnInit(): Promise<void> {
    await this.getDataAsync();
  }

  async getDataAsync(): Promise<void> {
    this.user = await lastValueFrom(this.userService.getUser());
  }

  onEdit(id?: number) {
    this.router.navigate(['/dashboard/admin/users/form', this.user.id])
  }

  async deleteUserAsync(): Promise<void> {
    const config = {
      title: 'Confirm exclusion',
      message: 'Do you really wish to delete this user?',
    } as ConfirmModalDialogConfig;
    this.confirmModalService.open(config);
    this.confirmModalService.closed.subscribe(async (result) => {
      if (result) {
        await lastValueFrom(this.userService.deleteUser());
        this.logout();
      }
    });
  }

  logout(){
    this.authService.logout();
    this.router.navigate(['/login']);
  }

}
