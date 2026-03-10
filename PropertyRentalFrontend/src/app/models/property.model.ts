export interface Property {
    id: number;
    name: string;
    address: string;
    city: string;
    monthlyPrice: number;
    isAvailable: boolean;
    createdAt: string;
}

export interface CreateProperty {
    name: string;
    address: string;
    city: string;
    monthlyPrice: number;
}

export interface UpdateProperty {
    name: string;
    address: string;
    city: string;
    monthlyPrice: number;
    isAvailable: boolean;
}
