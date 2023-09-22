import {
    OrderSavingModel,
    OrderViewModel,
    OrderEditingModel,
    OrderProductSavingModel, OrderProductModel
} from "../models/order-view-model";
import axios from "../utils/axiosConfig";
import {GetErrorMessage} from "../utils/constants-and-methods";
import {GetOrderRequestModel} from "../models/get-order-request-model";

export async function GetOrders( model: GetOrderRequestModel)
{
    return axios.post(`/orders`, model).then(response => {
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

export async function GetOrder(id :string)
{
    return axios.get(`/order/${id}`)
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

export async function InsertOrder(model: OrderSavingModel)
{
    return axios.post(`/order`, model).then(response => {
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

export function ClearSavedOrderProducts()
{
    localStorage.removeItem("products");
}
export function SaveOrderProduct(productSavingModels : OrderProductModel[])
{
    let index = -999;
    const products : OrderProductModel[] = [];
    const emptyOrderProduct: OrderProductModel = {
        id:0,
        productId: '',
        quantity: 0
    };
    productSavingModels.forEach(p=>{
        index+=1;
        p.id = index;
    })
    localStorage.setItem("products", JSON.stringify(productSavingModels));
}
export function HasSavedOrderProducts() {
    const productsStr = localStorage.getItem("products");
    return !!productsStr;
}
export function GetNumberOfSavedOrderProducts() {
    const productsStr = localStorage.getItem("products");
    if (productsStr) {
        const orderProducts : OrderProductModel[] = JSON.parse(productsStr);
        return orderProducts.length;
    }
    return 0;
}
export function GetSavedOrderProducts() {
    const productsStr = localStorage.getItem("products");
    if (productsStr) {
        const orderProducts : OrderProductModel[] = JSON.parse(productsStr);
        return orderProducts;
    }
    
    return null;
}

export async function UpdateOrder(model: OrderEditingModel)
{
    return axios.put(`/order/${model.OrderId}`, model).then(response => {
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