import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Lease, CreateLease } from '../models/lease.model';

@Injectable({ providedIn: 'root' })
export class LeaseService {
    private apiUrl = 'http://localhost:5030/api/leases';

    constructor(private http: HttpClient) { }

    create(lease: CreateLease): Observable<number> {
        return this.http.post<number>(this.apiUrl, lease);
    }

    getByProperty(propertyId: number): Observable<Lease[]> {
        return this.http.get<Lease[]>(`${this.apiUrl}/property/${propertyId}`);
    }

    endLease(id: number): Observable<void> {
        return this.http.put<void>(`${this.apiUrl}/end/${id}`, {});
    }
}
