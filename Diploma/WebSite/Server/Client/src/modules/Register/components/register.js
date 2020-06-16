import React from 'react';
import { Form, Button } from 'antd';
import { Link } from 'react-router-dom'
import { FormField, WhiteBlock } from 'components'
import './register.scss'

const Register = props => {
    const {
      values,
      touched,
      errors,
      handleChange,
      handleBlur,
      handleSubmit,
      isValid,
      dirty,
    } = props;
    
    return ( 
      <div>
           <div className="auth__top">
             <h2>Зареєструватися</h2>
             <p>Для входу, вам потрібно зареєструватися</p>
         </div>
         <WhiteBlock>
         <Form onSubmit={handleSubmit} className="login-form">
           <FormField 
           name="Email"
           placeholder="E-Mail"
           handleChange={handleChange}
           handleBlur={handleBlur}
           touched={touched}
           errors={errors}
           values={values}
           />
            <FormField 
           name="Login"
           placeholder="Ваше имя"
           handleChange={handleChange}
           handleBlur={handleBlur}
           touched={touched}
           errors={errors}
           values={values}
           />
            <FormField 
           name="Password"
           placeholder="Пароль"
           handleChange={handleChange}
           handleBlur={handleBlur}
           touched={touched}
           errors={errors}
           values={values}
           type="password"
           />
           {/* <FormField 
           name="password2"
           type="password"
           placeholder="Повторить пароль"
           handleChange={handleChange}
           handleBlur={handleBlur}
           touched={touched}
           errors={errors}
           values={values}
           /> */}
             <Form.Item>
             {dirty && !isValid ? <span>Ошибка!</span> : '' }
               <Button type="primary" onClick={handleSubmit} htmlType="submit" size="large" className="login-form-button" >
               Зареєструватися
               </Button>
               <Link to="/signIn" className="auth__register-link">Увійти в акаунт</Link>
             </Form.Item>
           </Form> 
           
         </WhiteBlock>
     </div>
      );
  }
   
  export default Register;