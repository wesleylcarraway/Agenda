import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SearchingForm } from './searching-form';
import { SearchingInputConfig } from './searching-input-config';

@Component({
  selector: 'app-searching',
  templateUrl: './searching.component.html',
  styleUrls: ['./searching.component.scss']
})
export class SearchingComponent {

  @Input() config!: SearchingInputConfig;

  form!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private cdRef: ChangeDetectorRef
  ) {
    this.form = this.formBuilder.group({
      field: [null, [Validators.required]],
      value: [null]
    });
  }

  search(): void {
    this.config.searchAction(this.form.value);
    this.cdRef.detectChanges();
  }

  clear(): void {
    this.form.get("value")?.setValue("");
    this.config.searchAction(new SearchingForm());
    this.cdRef.detectChanges();
  }
}
