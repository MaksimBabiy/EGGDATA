import React from 'react';
import './auth.scss'
import { Login, Register } from 'modules'
import { Route, Switch } from "react-router-dom";

const Auth = () =>  {

    return ( 
            <section className="auth">
                <div className="auth__content">
                <Switch>
                    <Route exact path={["/","/signIn"]} component={Login} />
                    <Route exact path="/signUp" component={Register}/>
                </Switch>
                </div>
            </section>
            );
    }
      
    export default Auth;