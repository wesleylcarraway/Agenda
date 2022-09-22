import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InteractionsComponent } from './interactions.component';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
  imports: [
    CommonModule,
    MatButtonModule
  ],
  declarations: [InteractionsComponent]
})
export class InteractionsModule { }
