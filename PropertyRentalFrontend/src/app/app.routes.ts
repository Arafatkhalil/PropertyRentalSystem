import { Routes } from '@angular/router';
import { PropertiesComponent } from './components/properties/properties.component';
import { TenantsComponent } from './components/tenants/tenants.component';
import { LeasesComponent } from './components/leases/leases.component';

export const routes: Routes = [
    { path: '', redirectTo: 'properties', pathMatch: 'full' },
    { path: 'properties', component: PropertiesComponent },
    { path: 'tenants', component: TenantsComponent },
    { path: 'leases', component: LeasesComponent }
];
