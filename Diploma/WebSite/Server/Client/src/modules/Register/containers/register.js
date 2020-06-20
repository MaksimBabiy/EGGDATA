import RegisterForm from '../components/register'
import { withFormik } from 'formik'

import { userActions } from 'redux/actions'

import store from 'redux/store'

export default  withFormik({
    mapPropsToValues: () => ({
        Email: '',
        Login: '',
        Password: '',
    }),
    handleSubmit: (values, { setSubmitting ,props }) => {
      store.dispatch(userActions.fetchUserRegister(values))
      .then(() => {
      
        setSubmitting(false)
      })
     
    },
    displayName: 'RegisterForm',
  })(RegisterForm);