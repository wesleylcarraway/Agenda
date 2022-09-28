import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { lastValueFrom } from 'rxjs';
import { BaseError } from '../shared/http-service/base-error';
import { UploadService } from '../shared/upload-service/upload.service';
import { apiErrorHandler } from '../shared/utils/api-error-handler';

@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.scss']
})
export class UploadFileComponent {
  files!: File[];

  constructor(
    private uploadService: UploadService,
    private snackBar: MatSnackBar
    ) { }

  async onChange(event: any) {
    const selectedFiles = <FileList>event.srcElement.files;
    this.files = new Array();
    for(let i = 0; i < selectedFiles.length; i++) {
      this.files.push(selectedFiles[i]);
    }
  }

  async uploadFile(): Promise<void> {
    try {
      if(this.files && this.files.length > 0) {
        await lastValueFrom(this.uploadService.upload(this.files));
        this.snackBar.open('File uploaded!', undefined, { duration: 3000 });
      }
    } catch ({ error }) {
      apiErrorHandler(this.snackBar, error as BaseError);
    }
  }
}
