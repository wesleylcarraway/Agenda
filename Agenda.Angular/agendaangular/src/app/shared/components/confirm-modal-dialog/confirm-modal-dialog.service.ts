import { EventEmitter, Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmModalDialogConfig } from './confirm-modal-dialog-config';
import { take } from 'rxjs/operators';
import { ConfirmModalDialogComponent } from './confirm-modal-dialog.component';

@Injectable({
  providedIn: 'root'
})
export class ConfirmModalDialogService {

  closed = new EventEmitter<boolean>();

  constructor(private matDialog: MatDialog) {}

  open(data: ConfirmModalDialogConfig): void {
    const dialog = this.matDialog.open(ConfirmModalDialogComponent, { data });

    dialog.afterClosed()
      .pipe(take(1))
      .subscribe((result: boolean) => {
        this.closed.emit(result);
      });
  }
}
