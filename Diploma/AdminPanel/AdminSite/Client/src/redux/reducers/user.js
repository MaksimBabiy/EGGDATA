const initialState = {
    data: null,
    token: window.localStorage.token,
    isAuth: !!window.localStorage.token,
    userData: JSON.parse(localStorage.getItem('userData')),
    id: null,
    tableValue: null
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
        case 'USER:SET_ID':
            return {
                ...state,
                id: payload
            }
        case 'USER:SET_TABLE_VALUE':
            return {
                ...state,
                tableValue: payload
            }
        default:
            return state
    }
}