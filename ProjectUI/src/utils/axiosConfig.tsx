import axios from 'axios';
import {environment} from "../env";

const axiosInstance = axios.create({
    baseURL: environment.apiUrl ,// Replace with your API base URL
    headers: {
        "Content-type": "application/json"
    }
});

axiosInstance.interceptors.response.use(
    response => response,
    error => {
        if (error.response) {
            console.error('Server Error:', error.response.data);
        } else if (error.request) {
            console.error('No Response:', error.request);
        } else {
            console.error('Error:', error.message);
        }

        return Promise.reject(error);
    }
);
export default axiosInstance;