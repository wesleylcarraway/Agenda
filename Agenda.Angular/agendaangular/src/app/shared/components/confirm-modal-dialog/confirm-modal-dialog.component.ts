import { Component, Inject} from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ConfirmModalDialogConfig } from './confirm-modal-dialog-config';

@Component({
  selector: 'app-confirm-modal-dialog',
  templateUrl: './confirm-modal-dialog.component.html',
  styleUrls: ['./confirm-modal-dialog.component.scss']
})
export class ConfirmModalDialogComponent {

  constructor(
    @Inject(MAT_DIALOG_DATA) public config: ConfirmModalDialogConfig,
    private matDialogRef: MatDialogRef<ConfirmModalDialogComponent>,
  ) { }

  confirm(confirmed: boolean): void {
    this.matDialogRef.close(confirmed);
  }

}
