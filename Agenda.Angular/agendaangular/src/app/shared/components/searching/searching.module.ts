import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchingComponent } from './searching.component';
import { MaterialModule } from '../../material/material.module';

@NgModule({
  imports: [
    CommonModule,
    MaterialModule
  ],
  declarations: [SearchingComponent],
  exports: [
    SearchingComponent
  ],
})
export class SearchingModule { }
