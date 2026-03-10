import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Property, CreateProperty, UpdateProperty } from '../models/property.model';

export interface PagedResult<T> {
    items: T[];
    totalCount: number;
    page: number;
    pageSize: number;
    totalPages: number;
}

@Injectable({ providedIn: 'root' })
export class PropertyService {
    private apiUrl = 'http://localhost:5030/api/properties';

    constructor(private http: HttpClient) { }

    getAll(city?: string, isAvailable?: boolean | null, page: number = 1, pageSize: number = 5): Observable<PagedResult<Property>> {
        let params = new HttpParams()
            .set('page', page.toString())
            .set('pageSize', pageSize.toString());
        if (city) params = params.set('city', city);
        if (isAvailable !== null && isAvailable !== undefined) params = params.set('isAvailable', isAvailable.toString());
        return this.http.get<PagedResult<Property>>(this.apiUrl, { params });
    }

    getById(id: number): Observable<Property> {
        return this.http.get<Property>(`${this.apiUrl}/${id}`);
    }

    create(property: CreateProperty): Observable<number> {
        return this.http.post<number>(this.apiUrl, property);
    }

    update(id: number, property: UpdateProperty): Observable<void> {
        return this.http.put<void>(`${this.apiUrl}/${id}`, property);
    }

    delete(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }
}
