import axios from "../utils/axiosConfig";
import {CustomerViewModel} from "../models/customer-view-model";
import {GetErrorMessage} from "../utils/constants-and-methods";

export async function deleteCustomer(id: string)
{
    return axios.delete(`/customer/${id}`)
        .then(response => {
            return {
                isOk: true,
                data: response.data,
                message: ''
            };
        })
        .catch(err => {
            return {

                isOk: false,
                data: undefined,
                message: GetErrorMessage(err)
            };
        });
}
export async function signIn(email: string, password: string) {
    // Send request
    return axios.post(`/login`, {email: email, password: password})
        .then(response => {
            if (response.data.token) {
                localStorage.setItem("user", JSON.stringify(response.data));
            }
            return {
                isOk: true,
                data: response.data, 
                message: ''
            };
        })
        .catch(err => {
            return {

                isOk: false,
                data: undefined,
                message: GetErrorMessage(err)
            };
        });
}

export function signOut() {
    // Send request
    try {
        localStorage.removeItem("user");
        return {
            isOk: true
        };
    } catch {
        return {
            isOk: false
        };
    }
}

export async function getUser() {
    const userStr = localStorage.getItem("user");
    if (userStr) {
        return {
            isOk: true,
            data: JSON.parse(userStr)
        };
    }

    return {
        isOk: false
    };
}

export async function createAccount(name: string, email: string, password: string) {
    // Send request
    const newUser: CustomerViewModel =
        {
            id: '',
            name: name,
            email: email,
            password: password
        };
    return axios.post(`/customer`, newUser)
        .then(response => {
            return {
                isOk: true,
                message: ""
            };
        }).catch(err => {
            return {
                isOk: false,
                message: GetErrorMessage(err)
            };
        });
}

