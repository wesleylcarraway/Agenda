import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LoadingModalConfig } from './loading-modal-config';

@Component({
  selector: 'app-loading-modal',
  templateUrl: './loading-modal.component.html',
  styleUrls: ['./loading-modal.component.scss']
})
export class LoadingModalComponent {

  constructor(
    @Inject(MAT_DIALOG_DATA) public config: LoadingModalConfig,
    private matDialogRef: MatDialogRef<LoadingModalComponent>,
  ) { }

  confirm(confirmed: boolean): void {
    this.matDialogRef.close(confirmed);
  }

}
