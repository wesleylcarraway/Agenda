import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
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
    return this.http.post<File[]>(`${this.env.API_URL}/upload`, files);
  }

}
