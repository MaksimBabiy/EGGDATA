import React from 'react'

import { Register } from 'modules'
import map from '../../assets/images/map.jpg'
import arrow from '../../assets/images/arrow.png'
import './Registration.css'

const Registration = () => {
    return (
        <section className="registration">
        <div className="container">
        <div className="row">
            <div className="col l6 m6 registration__info">
                <img src={map} alt="Наше местоположение" style={ {width: '100%'}} />
                <h2 className="registration__info--title center-align">Онлайн запись в удобном</h2>
                <h2 className="registration__info--title right-align">личном кабинете</h2>
                <h3 className="registration__info--subtitle right-align">Войти/Создать аккаунт</h3>
                <img src={arrow} className="right registration__info--arrow" alt="" />
            </div>
            <div className="col l6 m6 registration__form">
                {/* <h2 className="registration__form--title">Создай аккаунт сейчас</h2>
                <div className="registration__btns">
                <a className="waves-effect waves-light btn twitter">
                    TWITTER
                </a>
                <a className="waves-effect waves-light btn facebook">
                    Facebook
                </a>
                <a className="waves-effect waves-light btn google">
                    Google+
                </a>
                </div> */}
                <hr />
                <Register />
            </div>
        </div>
    </div>
</section>
    )
}

export default Registration
