import defaultUser from '../utils/default-user';
import axios from "axios";
import {environment} from "../env";


export async function signIn(email: string, password: string) {
  try {
    // Send request
    console.log(email, password);
    axios.get(`${environment.apiUrl}/login`).then(()=>{
      
    })
    

    return {
      isOk: true,
      data: defaultUser
    };
  }
  catch {
    return {
      isOk: false,
      message: "Authentication failed"
    };
  }
}

export async function getUser() {
  try {
    // Send request

    return {
      isOk: true,
      data: defaultUser
    };
  }
  catch {
    return {
      isOk: false
    };
  }
}

export async function createAccount(email: string, password: string) {
  try {
    // Send request
    console.log(email, password);

    return {
      isOk: true
    };
  }
  catch {
    return {
      isOk: false,
      message: "Failed to create account"
    };
  }
}

