import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Tenant, CreateTenant, UpdateTenant } from '../models/tenant.model';

@Injectable({ providedIn: 'root' })
export class TenantService {
    private apiUrl = 'http://localhost:5030/api/tenants';

    constructor(private http: HttpClient) { }

    getAll(): Observable<Tenant[]> {
        return this.http.get<Tenant[]>(this.apiUrl);
    }

    getById(id: number): Observable<Tenant> {
        return this.http.get<Tenant>(`${this.apiUrl}/${id}`);
    }

    create(tenant: CreateTenant): Observable<number> {
        return this.http.post<number>(this.apiUrl, tenant);
    }

    update(id: number, tenant: UpdateTenant): Observable<void> {
        return this.http.put<void>(`${this.apiUrl}/${id}`, tenant);
    }

    delete(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }
}
