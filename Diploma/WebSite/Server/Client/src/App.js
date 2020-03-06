import React from 'react';
import { Auth, Home } from '../src/pages'
import { Route,Redirect, Switch } from 'react-router-dom'
import { connect } from 'react-redux'
const App = () => {
  return (
    <>
    <Switch>
    <Route exact path={["/signIn","/signUp"]} component={Auth} />
    <Route 
    path="/"
    component={Home}/>
    </Switch>
    </>
  );
}

export default App;
