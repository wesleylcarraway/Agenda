import { EventEmitter, Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { LoadingModalConfig } from './loading-modal-config';
import { take } from 'rxjs/operators';
import { LoadingModalComponent } from './loading-modal.component';

@Injectable({
  providedIn: 'root'
})
export class LoadingModalService {

  closed = new EventEmitter<boolean>();

  constructor(private matDialog: MatDialog) {}

  open(data: LoadingModalConfig): void {
    const dialog = this.matDialog.open(LoadingModalComponent, { data });

    dialog.afterClosed()
      .pipe(take(1))
      .subscribe((result: boolean) => {
        this.closed.emit(result);
      });
  }

  close(): void {
    const dialog = this.matDialog.closeAll();
  }
}
