import {ProductViewModel} from "../models/product-view-model";
import axios from "../utils/axiosConfig";
import {GetErrorMessage} from "../utils/constants-and-methods";

export async function GetProducts(ids ?: string[]) {
    return axios.post(`/products`, ids??[]).then(response => {
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

export async function DeleteProduct(ids: string[]) {
    return axios.post(`/remove-products`, ids).then(response => {
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

export async function InsertProduct(model: ProductViewModel) {
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

