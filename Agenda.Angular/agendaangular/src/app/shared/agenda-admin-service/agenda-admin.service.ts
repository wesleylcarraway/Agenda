import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AdminContact } from '../entities/admin-contact';
import { Enumeration } from '../entities/enumeration';
import { HttpBaseService } from '../http-service/http-base.service';

@Injectable({
  providedIn: 'root'
})
export class AgendaAdminService extends HttpBaseService<AdminContact>{

  constructor(
    protected override http: HttpClient,
  ) {
    super("admin/agenda", http)
  }

  getPhoneTypes(): Observable<Enumeration[]> {
    return this.http.get<Enumeration[]>(`${this.env.API_URL}/agenda/phone-types`);
  }
}
