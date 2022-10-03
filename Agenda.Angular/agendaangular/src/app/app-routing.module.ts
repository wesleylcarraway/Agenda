import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { RouterModule, Routes } from "@angular/router";
import { HomeComponent } from './home/home.component';
import { LayoutComponent } from './shared/components/layout/layout.component';
import { LoginComponent } from './login/login.component';
import { AgendaComponent } from './agenda/agenda.component';
import { AuthGuard } from './shared/guards/auth.guard';
import { HttpInterceptorService } from './shared/interceptors/http-interceptor.service';
import { ContactFormComponent } from './shared/components/contact-form/contact-form.component';
import { AgendaAdminComponent } from './agenda-admin/agenda-admin.component';
import { AuthAdminGuard } from './shared/guards/auth-admin.guard';
import { UsersComponent } from './users/users.component';
import { UsersFormComponent } from './users/users-form/users-form.component';
import { RegisterComponent } from './register/register.component';
import { InteractionsComponent } from './interactions/interactions.component';
import { UploadFileComponent } from './upload-file/upload-file.component';
import { ProfileComponent } from './profile/profile.component';

const routes: Routes = [
  {
    path: 'dashboard',
    component: LayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: 'home', component: HomeComponent },
      { path: 'agenda', children: [
        {path: '', component: AgendaComponent },
        {path: 'form/:id', component: ContactFormComponent}
      ]
      },
      {path: 'interactions', component: InteractionsComponent},
      {path: 'upload', component: UploadFileComponent},
      {path: 'profile', component: ProfileComponent},
      {
        path: 'admin',
        canActivate: [AuthAdminGuard],
        children: [
          { path: 'agenda', children:[
            {path: '', component: AgendaAdminComponent },
            {path: 'form/:id', component: ContactFormComponent}
          ]
          },
          { path: 'users', children:[
            {path: '', component: UsersComponent },
            {path: 'form/:id', component: UsersFormComponent}
          ]
          },
        ],
      },
    ],
  },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '**', redirectTo: 'dashboard/home' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpInterceptorService,
      multi: true,
    },
  ],
})
export class AppRoutingModule {}
