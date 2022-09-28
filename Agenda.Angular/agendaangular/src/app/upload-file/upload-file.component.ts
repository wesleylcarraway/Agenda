import { Component, OnInit } from '@angular/core';
import { ConnectableObservable, lastValueFrom } from 'rxjs';
import { UploadService } from '../shared/upload-service/upload.service';

@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.scss']
})
export class UploadFileComponent {
  files!: File[];

  constructor(private uploadService: UploadService) { }

  async onChange(event: any) {
    //console.log(event);
    const selectedFiles = <FileList>event.srcElement.files;
    this.files = new Array<File>;
    for(let i = 0; i < selectedFiles.length; i++) {
      //console.log(selectedFiles[i].name)
      this.files.push(selectedFiles[i]);
      console.log(this.files[i].name)

    }
    await this.uploadFile();
  }

  async uploadFile(): Promise<void> {
    try {
      await lastValueFrom(this.uploadService.upload(this.files));
        //this.snackBar.open('Contact saved!', undefined, { duration: 3000 });
    } catch ({ error }) {
      //apiErrorHandler(this.snackBar, error as BaseError);
    }
  }

}
