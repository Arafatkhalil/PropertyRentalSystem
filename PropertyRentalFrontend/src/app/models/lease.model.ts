export interface Lease {
    id: number;
    propertyId: number;
    tenantId: number;
    startDate: string;
    endDate: string;
    monthlyPrice: number;
}

export interface CreateLease {
    propertyId: number;
    tenantId: number;
    startDate: string;
    endDate: string;
    monthlyPrice: number;
}
