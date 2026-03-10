import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TenantService } from '../../services/tenant.service';
import { Tenant, CreateTenant, UpdateTenant } from '../../models/tenant.model';

@Component({
    selector: 'app-tenants',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './tenants.component.html'
})
export class TenantsComponent implements OnInit {
    tenants: Tenant[] = [];
    showForm = false;
    isEditing = false;
    editId = 0;

    formData: CreateTenant = { fullName: '', phone: '', email: '', nationalId: '' };

    constructor(
        private tenantService: TenantService,
        private cdr: ChangeDetectorRef
    ) { }

    ngOnInit(): void {
        this.loadTenants();
    }

    loadTenants(): void {
        this.tenantService.getAll().subscribe(data => {
            this.tenants = data;
            this.cdr.detectChanges();
        });
    }

    openCreateForm(): void {
        this.showForm = true;
        this.isEditing = false;
        this.formData = { fullName: '', phone: '', email: '', nationalId: '' };
    }

    openEditForm(t: Tenant): void {
        this.showForm = true;
        this.isEditing = true;
        this.editId = t.id;
        this.formData = { fullName: t.fullName, phone: t.phone, email: t.email, nationalId: t.nationalId };
    }

    save(): void {
        if (this.isEditing) {
            const updateData: UpdateTenant = { ...this.formData };
            this.tenantService.update(this.editId, updateData).subscribe(() => {
                this.showForm = false;
                this.loadTenants();
            });
        } else {
            this.tenantService.create(this.formData).subscribe(() => {
                this.showForm = false;
                this.loadTenants();
            });
        }
    }

    deleteTenant(id: number): void {
        if (confirm('Are you sure you want to delete this tenant?')) {
            this.tenantService.delete(id).subscribe(() => this.loadTenants());
        }
    }

    cancel(): void {
        this.showForm = false;
    }
}
