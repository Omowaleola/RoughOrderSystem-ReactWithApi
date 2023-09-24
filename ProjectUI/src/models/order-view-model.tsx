export interface OrderViewModel {
    id: string;
    orderNo: string;
    paid: boolean;
    customer: string;
    total: number;
    createdOn: Date;
    products: OrderProductViewModel[];
}

export interface OrderGridViewModel {
    id: string;
    orderNo: string;
    paid: boolean;
    customer: string;
    total: number;
    createdOn: Date;
}

export interface OrderSavingModel {
    customerId: string;
    products: OrderProductSavingModel[];
}

export interface OrderEditingModel {
    OrderId: string;
    customerId: string;
    products: OrderProductSavingModel[];
}

export interface OrderProductSavingModel {
    productId: string;
    quantity: number;
}


export interface OrderProductModel {
    id: number;
    productId: string;
    quantity: number;
}

export interface OrderProductViewModel {
    id: number;
    productId: string;
    productPrice: number;
    quantity: number;
}