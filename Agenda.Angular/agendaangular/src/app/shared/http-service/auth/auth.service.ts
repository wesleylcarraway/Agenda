import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import jwtDecode from 'jwt-decode';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthUser } from '../../entities/auth-user';
import { JwtToken } from './jwt-token';
import { TokenProps } from './token-props';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  env = environment;

  constructor(private http: HttpClient) { }

  loginAsync(body: AuthUser): Observable<JwtToken> {
    return this.http.post<JwtToken>(`${this.env.API_URL}/auth`, body);
  }

  getToken(): string | null{
    return window.localStorage.getItem("@token");
  }

  getRole(): string | null{
    return window.localStorage.getItem("@role");
  }

  setToken(token: string): void {
    const { role } = jwtDecode(token) as TokenProps;
    window.localStorage.setItem("@token", token);
    window.localStorage.setItem("@role", role);
  }

  clearToken(): void {
    window.localStorage.removeItem("@token");
    window.localStorage.removeItem("@role");
  }

  logout(){
    window.localStorage.removeItem("@token");
    window.localStorage.removeItem("@role");
  }
}
