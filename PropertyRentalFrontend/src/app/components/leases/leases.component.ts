import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { LeaseService } from '../../services/lease.service';
import { PropertyService } from '../../services/property.service';
import { TenantService } from '../../services/tenant.service';
import { Lease, CreateLease } from '../../models/lease.model';
import { Property } from '../../models/property.model';
import { Tenant } from '../../models/tenant.model';

@Component({
    selector: 'app-leases',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './leases.component.html'
})
export class LeasesComponent implements OnInit {
    leases: Lease[] = [];
    properties: Property[] = [];
    tenants: Tenant[] = [];
    showForm = false;
    selectedPropertyId: number | null = null;
    errorMessage = '';

    formData: CreateLease = { propertyId: 0, tenantId: 0, startDate: '', endDate: '', monthlyPrice: 0 };

    constructor(
        private leaseService: LeaseService,
        private propertyService: PropertyService,
        private tenantService: TenantService,
        private cdr: ChangeDetectorRef
    ) { }

    ngOnInit(): void {
        this.loadAllProperties();
        this.tenantService.getAll().subscribe(data => {
            this.tenants = data;
            this.cdr.detectChanges();
        });
    }

    loadAllProperties(): void {
        this.propertyService.getAll(undefined, undefined, 1, 100).subscribe(result => {
            this.properties = result.items;
            this.cdr.detectChanges();
        });
    }

    loadLeases(): void {
        if (this.selectedPropertyId) {
            this.leaseService.getByProperty(this.selectedPropertyId).subscribe(data => {
                this.leases = data;
                this.cdr.detectChanges();
            });
        }
    }

    openCreateForm(): void {
        this.showForm = true;
        this.errorMessage = '';
        this.formData = { propertyId: 0, tenantId: 0, startDate: '', endDate: '', monthlyPrice: 0 };
    }

    save(): void {
        this.errorMessage = '';
        this.leaseService.create(this.formData).subscribe({
            next: () => {
                this.showForm = false;
                if (this.selectedPropertyId) this.loadLeases();
                this.loadAllProperties();
            },
            error: (err) => {
                this.errorMessage = err.error || 'An error occurred while creating the lease.';
                this.cdr.detectChanges();
            }
        });
    }

    endLease(id: number): void {
        if (confirm('Are you sure you want to end this lease?')) {
            this.leaseService.endLease(id).subscribe(() => {
                this.loadLeases();
                this.loadAllProperties();
            });
        }
    }

    cancel(): void {
        this.showForm = false;
        this.errorMessage = '';
    }
}
