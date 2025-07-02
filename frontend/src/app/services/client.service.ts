import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Client, SelfClientUpdate } from '../models/Client';


@Injectable({ providedIn: 'root' })
export class ClientService {
  private baseUrl = 'https://localhost:7046/api/Client';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Client[]> {
    return this.http.get<Client[]>(this.baseUrl);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  create(client: Client): Observable<Client> {
    return this.http.post<Client>(this.baseUrl, client);
  }

  update(id: number, client: Client): Observable<Client> {
    return this.http.put<Client>(`${this.baseUrl}/${id}`, client);
  }

  updateSelf(client: SelfClientUpdate): Observable<Client> {
    return this.http.put<Client>(`${this.baseUrl}/me`, client);
  }

  getOwnClient(): Observable<Client> {
    return this.http.get<Client>(this.baseUrl + '/me');
  }
}
