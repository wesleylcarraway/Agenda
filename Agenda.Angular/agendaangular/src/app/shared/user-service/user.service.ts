import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Enumeration } from '../entities/enumeration';
import { User } from '../entities/user';
import { HttpBaseService } from '../http-service/http-base.service';
import { take } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService extends HttpBaseService<User> {

  constructor(
    protected override http: HttpClient,
  ) {
    super("admin/users", http)
  }

  getUserRoles(): Observable<Enumeration[]> {
    return this.http.get<Enumeration[]>(`${this.env.API_URL}/${this.route}/user-roles`);
  }

  getAllUsersAsync(): Observable<User[]> {
    return this.http.get<User[]>(`${this.env.API_URL}/${this.route}/all`);
  }

  getUser() {
    return this.http.get<User>(`${this.env.API_URL}/${this.route}/get-user`).pipe(take(1));
  }

  deleteUser() {
    return this.http.delete<void>(`${this.env.API_URL}/${this.route}/delete-user`).pipe(take(1));
  }
}
