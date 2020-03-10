import { axios } from 'core';
export default {
    signIn: ({Email : UserName, Password}) => axios.post('api/token/auth', {UserName, Password,
    GrantType: "password",
    ClientId: "User",
    scope: "offline_access profile email"
    }),
    signUp: obj => axios.put('api/Account/RegisterUser', obj)
}