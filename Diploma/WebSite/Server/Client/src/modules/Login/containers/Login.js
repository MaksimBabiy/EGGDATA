import Login from '../components/Login'
import { withFormik } from 'formik'
import validation from 'utils/validation'

import { userActions } from 'redux/actions'

import store from 'redux/store'

const LoginContainer = withFormik({
    mapPropsToValues: () => ({
        email: '',
        password: ''
    }),
    validate: values => {
      let errors = {};
      validation({ isAuth: true,values,errors })
      return errors
    },
    handleSubmit: (values, {setSubmitting, props}) => {
      store.dispatch(userActions.fetchUserLogin(values))
      .then( ({status}) => {
        if(status === 'success'){
            props.history.push('/')
        }
        setSubmitting(false)
      })
    },
    displayName: 'Login',
  })(Login);

  

  export default LoginContainer;