import React from 'react';
import { Form, Button, Input } from 'antd';
import { Link } from 'react-router-dom'
import { FormField, WhiteBlock } from 'components'
import { UserOutlined,LockOutlined,ArrowLeftOutlined } from '@ant-design/icons';
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
         <Form.Item 
           validateStatus={!touched.email ? '' : errors.email ? 'error' : 'success'} 
           hasFeedback
           help={!touched.email ? '' : errors.email}>
               <Input
                 id="Email"
                 size="large"
                 prefix={<UserOutlined />}
                 placeholder="E-Mail"
                 value={values.email}
                 onChange={handleChange}
                 onBlur={handleBlur}
               />
           </Form.Item>
           <Form.Item >
               <Input
                 id="Login"
                 size="large"
                 type="text"
                 value={values.login}
                 placeholder="Login"
                 onChange={handleChange}
                 onBlur={handleBlur}
               />
           </Form.Item>
             <Form.Item 
           validateStatus={!touched.password ? '' : errors.password ? 'error' : 'success'} 
           hasFeedback
           help={!touched.password ? '' : errors.password}>
               <Input
                 id="Password"
                 size="large"
                 type="password"
                 value={values.password}
                 prefix={<LockOutlined />}
                 placeholder="Пароль"
                 onChange={handleChange}
                 onBlur={handleBlur}
               />
           </Form.Item>
             <Form.Item>
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