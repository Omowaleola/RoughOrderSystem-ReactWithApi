export interface CustomerViewModel
{
    id : string;
    name : string;
    email : string;
    password : string;
    userRole ?: number;
}
export interface CustomerReturnModel
{
    id : string;
    name : string;
    email : string;
}
export interface CustomerLoggedInModel
{
    id : string;
    name : string;
    email : string;
    userRole : number;
    token : string;
    tokenExpiry : Date;
}