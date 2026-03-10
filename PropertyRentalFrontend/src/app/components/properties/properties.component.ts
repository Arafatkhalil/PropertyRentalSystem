import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PropertyService } from '../../services/property.service';
import { Property, CreateProperty, UpdateProperty } from '../../models/property.model';

@Component({
    selector: 'app-properties',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './properties.component.html'
})
export class PropertiesComponent implements OnInit {
    properties: Property[] = [];
    showForm = false;
    isEditing = false;
    editId = 0;

    // Pagination
    currentPage = 1;
    pageSize = 5;
    totalPages = 0;
    totalCount = 0;

    // Filter fields
    filterCity = '';
    filterAvailability: string = '';

    formData: CreateProperty = { name: '', address: '', city: '', monthlyPrice: 0 };

    constructor(
        private propertyService: PropertyService,
        private cdr: ChangeDetectorRef
    ) { }

    ngOnInit(): void {
        this.loadProperties();
    }

    loadProperties(): void {
        const city = this.filterCity || undefined;
        const isAvailable = this.filterAvailability === '' ? null :
            this.filterAvailability === 'true';

        this.propertyService.getAll(city, isAvailable, this.currentPage, this.pageSize).subscribe(result => {
            this.properties = result.items;
            this.totalCount = result.totalCount;
            this.totalPages = result.totalPages;
            this.cdr.detectChanges();
        });
    }

    goToPage(page: number): void {
        if (page >= 1 && page <= this.totalPages) {
            this.currentPage = page;
            this.loadProperties();
        }
    }

    clearFilters(): void {
        this.filterCity = '';
        this.filterAvailability = '';
        this.currentPage = 1;
        this.loadProperties();
    }

    openCreateForm(): void {
        this.showForm = true;
        this.isEditing = false;
        this.formData = { name: '', address: '', city: '', monthlyPrice: 0 };
    }

    openEditForm(p: Property): void {
        this.showForm = true;
        this.isEditing = true;
        this.editId = p.id;
        this.formData = { name: p.name, address: p.address, city: p.city, monthlyPrice: p.monthlyPrice };
    }

    save(): void {
        if (this.isEditing) {
            const updateData: UpdateProperty = { ...this.formData, isAvailable: true };
            this.propertyService.update(this.editId, updateData).subscribe(() => {
                this.showForm = false;
                this.loadProperties();
            });
        } else {
            this.propertyService.create(this.formData).subscribe(() => {
                this.showForm = false;
                this.loadProperties();
            });
        }
    }

    deleteProperty(id: number): void {
        if (confirm('Are you sure you want to delete this property?')) {
            this.propertyService.delete(id).subscribe(() => this.loadProperties());
        }
    }

    cancel(): void {
        this.showForm = false;
    }

    getPageNumbers(): number[] {
        const pages: number[] = [];
        for (let i = 1; i <= this.totalPages; i++) {
            pages.push(i);
        }
        return pages;
    }
}
