import React from 'react'
import { Form, Input } from 'antd';
import { Link } from 'react-router-dom';
import { UserOutlined,LockOutlined,ArrowLeftOutlined } from '@ant-design/icons';
import { WhiteBlock, Button } from 'components'
import './login.scss'
const Login = (props) => {
    const { 
        values,
        touched,
        errors,
        handleChange,
        handleBlur,
        handleSubmit,
    } = props
  return (
      <div>
           <WhiteBlock>
           <Link to="/"><ArrowLeftOutlined style={{fontSize: 25}} className="icon"/></Link>
         <div className="auth__top">
         <h2>Увійти в акаунт</h2>
           <p>Будь ласка, увійдіть в свій аккаунт</p>
       </div>
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
           <Button type="primary" htmlType="submit" size="large" className="login-form-button" onClick={handleSubmit} >
            Увійти
             </Button>
             <Link to="/signUp" className="auth__register-link" >Зареєструватися</Link>
           </Form.Item>
         </Form>
         </WhiteBlock>
   </div>
    )
}
export default Login
