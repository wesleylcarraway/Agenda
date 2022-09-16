import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { Roles } from "../enums/roles";
import { AuthService } from "../http-service/auth/auth.service";

@Injectable({
  providedIn: 'root'
})
export class AuthAdminGuard implements CanActivate {

  constructor(
    public router: Router,
    //private confirmModalService: ConfirmModalService,
    private authService: AuthService
  ) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const role = this.authService.getRole();
    const isAdmin = role == Roles.ADMIN;
    if (!isAdmin) {
      //this.showUnauthorizedMessage();
      this.router.navigate(['dashboard', 'home']);
    }
    return isAdmin;
  }

  /*showUnauthorizedMessage(): void {
    this.confirmModalService.open({
      title: 'Ops...',
      message: 'Acesso negado',
    });
    this.confirmModalService.closed
      .pipe(take(1))
      .subscribe();
  }*/

}
