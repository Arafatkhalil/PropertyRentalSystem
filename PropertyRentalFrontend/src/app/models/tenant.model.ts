export interface Tenant {
    id: number;
    fullName: string;
    phone: string;
    email: string;
    nationalId: string;
}

export interface CreateTenant {
    fullName: string;
    phone: string;
    email: string;
    nationalId: string;
}

export interface UpdateTenant {
    fullName: string;
    phone: string;
    email: string;
    nationalId: string;
}
