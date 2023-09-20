export interface OrderViewModel
{
    id : string;
    paid : boolean;
    customerId: string;
    total: number;
    createdOn ?: Date;
    products : string[];
}