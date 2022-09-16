import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../entities/user';
import { HttpBaseService } from '../http-service/http-base.service';

@Injectable({
  providedIn: 'root'
})
export class RegisterService extends HttpBaseService<User> {

  constructor(
    protected override http: HttpClient,
  ) {
    super("common-users", http)
  }

  registerAsync(user: User): Observable<User> {
    const body = Object.assign(user, {});
    return this.http.post<User>(`${this.env.API_URL}/${this.route}`, body);
  }
}
