export function GetErrorMessage(error : any)
{
    if (error.response) {
        return `Server Error: ${error.response.data} `;
    } else if (error.request) {
        return `No Response: ${error.request}`;
    } else {
        return `Error: ${error.message}`;
    }
}