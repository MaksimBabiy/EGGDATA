import Login from '../components/Login'
import { withFormik } from 'formik'
import validation from 'utils/validation'

import { userActions } from 'redux/actions'

import store from 'redux/store'

const LoginContainer = withFormik({
    mapPropsToValues: () => ({
        Email: '',
        Password: ''
    }),
    validate: values => {
      let errors = {};
      validation({ isAuth: true,values,errors })
      return errors
    },
    handleSubmit: (values, {setSubmitting, props}) => {
      store.dispatch(userActions.fetchUserLogin(values))
      .then( () => {
        props.history.push("/")
        setSubmitting(false)
      })
    },
    displayName: 'Login',
  })(Login);

  

  export default LoginContainer;