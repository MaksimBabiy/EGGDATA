import React from 'react';
import { Auth, Home } from '../src/pages'
import { Route, Switch } from 'react-router-dom'
import { DoctorsPage, Header, Footer, Doctors,News,Adventages } from 'components';
const App = () => {
  return (
    <>
    <Header />
    <Switch>
    <Route exact path={["/signIn","/signUp"]} component={Auth} />
    <Route exact path={"/doctors"} component={DoctorsPage} />
    <Route path={"/doctor/:id"} component={Doctors}/>
    <Route path={"/news"} component={News}/>
    <Route path={"/FAQ"} component={Adventages}/>
    <Route path="/"component={Home}/>
    </Switch>
    <Footer />
    </>
  );
}

export default App;
