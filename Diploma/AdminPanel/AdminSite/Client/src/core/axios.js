import axios from 'axios'

axios.defaults.baseURL = window.location.origin;
console.log(axios.defaults.baseURL)
axios.defaults.headers.common['token'] = window.localStorage.token;

window.axios = axios
export default axios