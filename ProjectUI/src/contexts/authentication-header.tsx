export default function AuthenticationHeader() {
    const userStr = localStorage.getItem("user");
    let user = null;
    if (userStr)
        user = JSON.parse(userStr);

    if (user && user.accessToken) {
        return { Authorization: 'Bearer ' + user.token }; 
    } else {
        return { Authorization: '' }; 
    }
}