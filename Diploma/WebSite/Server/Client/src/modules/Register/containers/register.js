import RegisterForm from '../components/register'
import { withFormik } from 'formik'
import validation from 'utils/validation'
import { userActions } from 'redux/actions'
import get from 'lodash/get';
import store from 'redux/store'

export default  withFormik({
    mapPropsToValues: () => ({
        Email: '',
        Login: '',
        Password: '',
        // password2: ''
    }),
    validate: values => {
      let errors = {};
      validation({ isAuth: false, values, errors })
      return errors
    },
    handleSubmit: (values, { setSubmitting,props }) => {
      store.dispatch(userActions.fetchUserRegister(values))
      .then(() => {
        props.history.push("/signIn");
        setSubmitting(false)
      })
      .catch(err => {
        if (get(err, 'response.data.message.errmsg', '').indexOf('dup') >= 0) {
         console.log({
            title: 'Ошибка',
            text: 'Аккаунт с такой почтой уже создан.',
            type: 'error',
            duration: 5000
          });
        } else {
          console.log({
            title: 'Ошибка',
            text: 'Возникла серверная ошибка при регистрации. Повторите позже.',
            type: 'error',
            duration: 5000
          });
        }
        setSubmitting(false);
      });
    },
    displayName: 'RegisterForm',
  })(RegisterForm);