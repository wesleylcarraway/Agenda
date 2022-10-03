import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileComponent } from './profile.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import {MatCardModule} from '@angular/material/card';
import { UsersModule } from '../users/users.module';

@NgModule({
  imports: [
    CommonModule,
    MatExpansionModule,
    MatIconModule,
    MatButtonModule,
    MatCardModule,
    UsersModule,
    HttpClientModule,
    RouterModule,
  ],
  declarations: [ProfileComponent]
})
export class ProfileModule { }
