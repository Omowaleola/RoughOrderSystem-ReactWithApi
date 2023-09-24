export interface ProductViewModel
{
    id ?: string;
    name : string;
    description : string;
    price: number;
}

export interface ProductSavingModel
{
    name : string;
    description : string;
    price: number;
}

export interface RequestModel
{
    ids ?: string[];
}

export interface ProductDeletingModel
{
    ids : string[];
}