
import { userApi } from 'utils/api'
const actions = {
    setUserData: user => ({
        type: 'USER:SET_DATA',
        payload: user
    }),
    setIsAuth: bool => ({
      type: 'USER:SET_IS_AUTH',
      payload: bool
  }),
  fetchUserLogin: obj => dispatch => {
    return userApi.signIn(obj)
    .then(({data}) => {
        const {status, token} = data;
        if(status === "error"){
            console.log({
              title: "Ошибка при авторизации",
              text: "Неверный логин или пароль", 
              type: "error"
            })
          }
          else {
            console.log({
              text: "Отлично!",
              title: "Авторизация успешна!",
              type: "success"
            })
          dispatch(actions.setUserData(data))
          window.axios.defaults.headers.common["token"] = token;
          window.localStorage["token"] = token
          dispatch(actions.setIsAuth(true))
            //   dispatch(actions.fetchUserData())
        }
        return data
    })
},
fetchUserRegister: obj => dispatch =>{
  return userApi.signUp(obj)
}, 
}
export default actions