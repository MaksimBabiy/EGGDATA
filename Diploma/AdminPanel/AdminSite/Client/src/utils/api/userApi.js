import { axios } from 'core';
export default {
    signIn: ({Email : UserName, Password}) => axios.post('api/admintoken/auth', {UserName, Password,
    GrantType: "password",
    ClientId: "User",
    scope: "offline_access profile email"
    })
}