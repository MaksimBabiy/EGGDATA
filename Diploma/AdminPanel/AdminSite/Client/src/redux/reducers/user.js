const initialState = {
    data: null,
    token: window.localStorage.token,
    isAuth: !!window.localStorage.token,
    userData: JSON.parse(localStorage.getItem('userData'))
}

export default ( state = initialState, {type, payload}) => {
    switch (type) {
        case 'USER:SET_DATA':
            return {
                ...state,
               data: payload,
               isAuth: true,
               token: window.localStorage.token,
            };
        case 'USER:SET_NAME': 
        return {
            ...state,
            userData: payload,
        };
        case 'USER:SET_IS_AUTH':
            return {
                ...state,
                isAuth: payload
            };
        default:
            return state
    }
}