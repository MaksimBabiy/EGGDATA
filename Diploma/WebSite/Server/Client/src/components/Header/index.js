import React, { useState } from 'react'
import { Link } from 'react-router-dom'
import logo from '../../assets/images/logo1.png'
import './Header.css'
const Header = () => {
    const [isLogin, setIsLogin] = useState(false)
    return (
        <header className="main-header">
        <div className="navbar-fixed">
            <nav className="transparent">
                <div className="container">
                    <div className="nav-wrapper">
                        <a  className="brand-logo"><img src={logo} height="50" width="50" alt="" />ECGData</a>
                        <a href="#" data-target="mobile-nav" className="sidenav-trigger">
                            <i className="material-icons">menu</i>
                        </a>
                        <ul className="right hide-on-med-and-down">
                            <li>
                                <a >О нас</a>
                            </li>
                            <li>
                                <a className="link-menu" >Доктора</a>
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
                                { !isLogin ? 
                                <Link className="link-menu" to="signIn">Войти</Link>
                                :
                                <a className="link-menu" >Выйти</a>
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

export default Header
