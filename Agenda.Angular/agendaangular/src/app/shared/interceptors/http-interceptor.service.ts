import { HttpEvent, HttpHandler, HttpRequest, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError, take } from 'rxjs/operators';
import { AuthService } from '../http-service/auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class HttpInterceptorService {

  constructor(
    private authService: AuthService,
    //private confirmModalService: ConfirmModalService,
    private router: Router
  ) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.authService.getToken();
    if (token) {
      req = this.setToken(req, token);
    }
    return next.handle(req)
      .pipe(
        catchError((error: HttpEvent<any>) => {
          if (error instanceof HttpErrorResponse) {
            this.error401Handler(error);
          }
          return throwError(error);
        })
      );
  }

  error401Handler(error: HttpErrorResponse): void {
    if (error.status !== 401) {
      return;
    }
    const config = {
      title: "Inspired session",
      message: "Your session has expired! Please login again to the system.",
    } /*as ConfirmModalConfig;
    this.confirmModalService.open(config);
    this.confirmModalService.closed
      .pipe(take(1))
      .subscribe(() => {
        this.authService.clearToken();
        this.router.navigate(['login']);
      });*/
  }

  setToken(req: HttpRequest<any>, token: string): HttpRequest<any> {
    return req.clone({ headers: req.headers.set('Authorization', `Bearer ${token}`) });
  }
}
