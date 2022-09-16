import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Contact } from '../entities/contact';
import { Enumeration } from '../entities/enumeration';
import { HttpBaseService } from '../http-service/http-base.service';

@Injectable({
  providedIn: 'root'
})
export class AgendaService extends HttpBaseService<Contact>{

  constructor(
    protected override http: HttpClient,
  ) {
    super("agenda", http)
  }

  getPhoneTypes(): Observable<Enumeration[]> {
    return this.http.get<Enumeration[]>(`${this.env.API_URL}/${this.route}/phone-types`);
  }
}
