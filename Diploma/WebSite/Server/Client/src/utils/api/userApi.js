import { axios } from 'core';

export default {
    signIn: obj => axios.post('api/Account/Login', obj), 
    signUp: obj => axios.post('api/Account/RegisterUser', obj),
}