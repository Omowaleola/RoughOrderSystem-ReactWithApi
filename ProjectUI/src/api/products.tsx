import {ProductDeletingModel, ProductSavingModel, ProductViewModel, RequestModel} from "../models/product-view-model";
import axios from "../utils/axiosConfig";
import {GetErrorMessage} from "../utils/constants-and-methods";

export async function GetProducts(ids ?: string[]) {
    const model : RequestModel =
        {
            ids: ids??[]
        };
    return axios.post(`/products`, model ).then(response => {
        return {
            
            isOk: true,
            data: response.data
        };
    }).catch(err => {
        return {
            isOk: true,
            data: err.data
        };
    });
}

export async function GetProduct(id: string) {
    return axios.get(`/product/${id}`)
        .then(response => {
            return {
                isOk: true,
                data: response.data
            };
        }).catch(err => {
            return {
                isOk: true,
                data: GetErrorMessage(err)
            };
        })
}

export async function DeleteProduct(ids: string[] ) {
    const model : ProductDeletingModel =
        {
            ids: ids
        };
    return axios.post(`/remove-products`, model).then(response => {
        return GetProducts();
        // return {
        //     isOk: true,
        //     data: response.data
        // };
    }).catch(err => {
        return {
            isOk: true,
            data: GetErrorMessage(err)
        };
    });
}

export async function InsertProduct(model: ProductSavingModel) {
    return axios.post(`/product`, model).then(response => {
        
        return {
            isOk: true,
            data: response.data
        };
    }).catch(err => {
        return {
            isOk: true,
            data: GetErrorMessage(err)
        };
    });
}

export async function UpdateProduct(model: ProductViewModel) {
    return axios.put(`/product/${model.id}`, model)
        .then(response => {
            return GetProducts();
            // return {
            //     isOk: true,
            //     data: response.data
            // };
        }).catch(err => {
            return {
                isOk: true,
                data: GetErrorMessage(err)
            };
        });
}

