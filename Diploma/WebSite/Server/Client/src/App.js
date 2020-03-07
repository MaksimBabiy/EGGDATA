import React from 'react';
import { Auth, Home } from '../src/pages'
import { Route, Switch } from 'react-router-dom'
import { DoctorsPage } from 'components';
const App = () => {
  return (
    <>
    <Switch>
    <Route exact path={["/signIn","/signUp"]} component={Auth} />
    <Route exact path={"/doctors"} component={DoctorsPage} />
    <Route path="/"component={Home}/>
    </Switch>
    </>
  );
}

export default App;
