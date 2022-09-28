import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LayoutModule } from './shared/components/layout/layout.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginModule } from './login/login.module';
import { AgendaModule } from './agenda/agenda.module';
import { RegisterModule } from './register/register.module';
import { InteractionsModule } from './interactions/interactions.module';
import { UploadFileModule } from './upload-file/upload-file.module';

@NgModule({
  declarations: [
    AppComponent,

   ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    LayoutModule,
    AppRoutingModule,
    LoginModule,
    AgendaModule,
    RegisterModule,
    InteractionsModule,
    UploadFileModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
