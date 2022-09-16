import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { BaseParams } from './base-params';
import { PaginationResponse } from './pagination-response';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpBaseService<T> {
  env = environment;

  constructor(
    @Inject("route") protected route: string,
    protected http: HttpClient,
    ) { }

  getAsync(params = new BaseParams()): Observable<PaginationResponse<T>> {
    return this.http.get<PaginationResponse<T>>(`${this.env.API_URL}/${this.route}`, { params });
  }

  getByIdAsync(id: number): Observable<T> {
    return this.http.get<T>(`${this.env.API_URL}/${this.route}/${id}`);
  }

  createAsync(data: T): Observable<T> {
    const body = Object.assign(data, {});
    return this.http.post<T>(`${this.env.API_URL}/${this.route}`, body);
  }

  updateAsync(data: T, id: number): Observable<T> {
    const body = Object.assign(data, {})
    return this.http.put<T>(`${this.env.API_URL}/${this.route}/${id}`, body);
  }

  deleteAsync(id: number): Observable<void> {
    return this.http.delete<void>(`${this.env.API_URL}/${this.route}/${id}`);
  }
}
