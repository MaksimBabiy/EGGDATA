import React, { useState } from 'react'
import { Link } from 'react-router-dom'
import logo from '../../assets/images/logo1.png'
import { connect } from 'react-redux'
import './Header.css'
import 'utils/scroll'
import { userActions } from 'redux/actions'
const Header = ({isAuth, setIsAuth}) => {
    console.log(setIsAuth)
    return (
        <header className="main-header">
        <div className="navbar-fixed">
            <nav className="transparent">
                <div className="container">
                    <div className="nav-wrapper">
                        <Link className="brand-logo" to="/"><img src={logo} height="50" width="50" alt="" />ECGData</Link>
                        <a href="#" data-target="mobile-nav" className="sidenav-trigger">
                            <i className="material-icons">menu</i>
                        </a>
                        <ul className="right hide-on-med-and-down">
                            <li>
                                <a >О нас</a>
                            </li>
                            <li>
                                <Link className="link-menu" to="/doctors">Доктора</Link>
                            </li>
                            <li>
                                <a  >Новости</a>
                            </li>
                            <li>
                                <a >Ответы</a>
                            </li>
                            <li>
                                <a >Контакты</a>
                            </li>     
                            <li>
                                <Link to="admin_panel">Панель администратора</Link>
                            </li>                        
                            <li>
                                { !isAuth ? 
                                <Link className="link-menu" to="signIn">Войти</Link>
                                :
                                <a className="link-menu" onClick={() => {
                                    setIsAuth(false)
                                    window.localStorage.clear()
                                }}>Выйти</a>
                                }
                            </li>                                                
                        </ul>
                    </div>
                </div>  
            </nav>
        </div>

         <ul className="sidenav" id="mobile-nav">
            <li><a href="#">О НАС</a></li>
            <li><a href="#">НОВОСТИ</a></li>
            <li><a href="#">СОБЫТИЯ</a></li>
            <li><a href="#">КОНТАКТЫ</a></li>
            <li><a href="#">ВОЙТИ</a></li>
        </ul>
</header>
    )
}


export default connect(({user }) => ({
    isAuth: user.isAuth
  }),userActions)(Header);
