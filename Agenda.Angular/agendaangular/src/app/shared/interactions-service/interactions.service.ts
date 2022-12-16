import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Interaction } from '../entities/interaction';
import { HttpBaseService } from '../http-service/http-base.service';

@Injectable({
  providedIn: 'root'
})
export class InteractionsService extends HttpBaseService<Interaction>{

  constructor(
    protected override http: HttpClient,
  ) {
    super("interactions", http)
  }

  getInteractionsAsync(): Observable<Interaction[]> {
    return this.http.get<Interaction[]>(`${this.env.API_URL}/${this.route}`);
  }
}
