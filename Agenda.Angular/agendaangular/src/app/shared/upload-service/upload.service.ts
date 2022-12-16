import { HttpClient} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UploadService {
  env = environment;

  constructor(
    protected http: HttpClient,
  ) { }

  upload(files: File[]): Observable<File[]> {
    const formData = new FormData();
    files.forEach(file => formData.append('files', file, file.name));
    return this.http.post<File[]>(`${this.env.API_URL}/upload`, formData);
  }
}
