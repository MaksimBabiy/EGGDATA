import React from 'react';
import { Auth, Home } from '../src/pages'
import { Route, Switch, Redirect } from 'react-router-dom'
import { DoctorsPage, Header, Footer, AdminPanel,TestPage } from 'components';
import { connect } from 'react-redux'
const App = ({isAuth}) => {

  return (
    <>
    <Header />
    <Switch>
    <Route exact path={["/signIn","/signUp"]} component={Auth} />
    <Route exact path="/admin_panel" render={() => (isAuth ? <AdminPanel /> : <Redirect to="/"/>)} />
    <Route exact path={"/doctors"} component={DoctorsPage} />
    <Route exact path={"/test"} component={TestPage} />
    <Route path="/"component={Home}/>
    </Switch>
    <Footer />
    </>
  );
}

export default connect(({user}) =>(
  {
    isAuth: user.isAuth
  }
))(App)
