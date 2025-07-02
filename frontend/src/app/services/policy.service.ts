// src/app/services/policy.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Policy } from '../models/Policy';

@Injectable({ providedIn: 'root' })
export class PolicyService {
  private apiUrl = 'https://localhost:7046/api/Policy';

  constructor(private http: HttpClient) {}

  getPolicies(): Observable<Policy[]> {
    return this.http.get<Policy[]>(this.apiUrl);
  }

  create(policy: Policy): Observable<Policy> {
    return this.http.post<Policy>(this.apiUrl, policy);
  }

  update(id: number, policy: Policy): Observable<Policy> {
    return this.http.put<Policy>(`${this.apiUrl}/${id}`, policy);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  getOwnPolicies(): Observable<Policy[]> {
    return this.http.get<Policy[]>(`${this.apiUrl}/mine`);
  }

  CancelPolicy(policyId: number) {
    return this.http.put(`${this.apiUrl}/cancel/${policyId}`, {}); // No se necesita body
  }

}
