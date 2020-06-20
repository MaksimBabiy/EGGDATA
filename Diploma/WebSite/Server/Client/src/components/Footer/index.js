import React from 'react'

import './Footer.css'
const Footer = () => {
    return (
        
<footer className="blue-grey darken-2 white-text">
        <section className="call blue white-text">
                <div className="container">
                    <div className="call__text">
                        <h3 className="call__title">Свяжитесь с нами</h3>
              
                    </div>
                    <div className="call__button">
                        <button className="btn waves-effect waves-light white blue-text">ПОЗВОНИТЬ</button>
                    </div>
                    <div className="call__number">
                        <i className="material-icons left">call</i>
                        <h3>Связаться по номеру</h3>
                        <h2>093 345 12 11</h2>
                    </div>
                </div>
            </section>
            
        <div className="container">
            <div className="row">
                <div className="col l3 m6 s12">
                    <h5>Быстрые ссылки</h5>
                    <div className="row">
                        <ul className="col m6">
                            <li><a href="">Доктора</a></li>
                            <li><a href="">Возможности</a></li>
                            <li><a href="">Галерея</a></li>
                            <li><a href="">Отзывы</a></li>
                        </ul>
                        <ul className="col m6">
                            <li><a href="">Правила и политика использования</a></li>
                            <li><a href="">Поиск</a></li>
                            <li><a href="">Карта сайта</a></li>
                        </ul>
                    </div>
                </div>
                <div className="col l3 m6 s12">
                    <h5>Подписаться на рассылку</h5>
                  
                    <form>
                        <input type="email" className="subscribe-email" name="email" placeholder="E-mail Address" />
                        <input type="submit" value=" " className="subscribe-submit" style={{ background: "url(../../../assets/images/subscribe.png)"}} />
                    </form>
                </div>
                <div className="col l3 m6 s12">
                    <h5>Время работы:</h5>
                    <table>
                        <tr>
                            <td>Понедельник-Пятница</td>
                            <td>9:00 - 18:00</td>
                        </tr>
                        <tr>
                            <td>Суббота</td>
                            <td>9:00 - 16:00</td>
                        </tr>
                        <tr>
                            <td>Воскресенье</td>
                            <td>9:00 - 14:00</td>
                        </tr>
                    </table>
                </div>
                <div className="col l3 m6 s12">
                    <h5>Связаться с нами</h5>
                  
                    <p>Телефон: +380933451211</p>
                    <p>E-mail: info@ecgdata.ru</p>
                </div>
            </div>
            <hr />
            <p className="center">
             
            </p>
        </div>
</footer>
    )
}

export default Footer
