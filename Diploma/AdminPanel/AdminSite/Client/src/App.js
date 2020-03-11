import React from 'react';
import { Auth, Home } from '../src/pages'
import { Route, Switch } from 'react-router-dom'
import { DoctorsPage, Header, Footer, AdminPanel } from 'components';
const App = () => {
  return (
    <>
    <Header />
    <Switch>
    <Route exact path={["/signIn","/signUp"]} component={Auth} />
    <Route exact path={"/admin_panel"} component={AdminPanel} />
    <Route exact path={"/doctors"} component={DoctorsPage} />
    <Route path="/"component={Home}/>
    </Switch>
    <Footer />
    </>
  );
}

export default App;
