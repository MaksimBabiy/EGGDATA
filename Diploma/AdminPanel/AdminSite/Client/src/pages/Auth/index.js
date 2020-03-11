import React from 'react';
import './auth.scss'
import { Login } from 'modules'
import { Route, Switch } from "react-router-dom";

const Auth = () =>  {

    return ( 
            <section className="auth">
                <div className="auth__content">
                <Switch>
                    <Route exact path={["/","/signIn"]} component={Login} />
                </Switch>
                </div>
            </section>
            );
    }
      
    export default Auth;