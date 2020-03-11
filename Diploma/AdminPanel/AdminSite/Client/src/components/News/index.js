import React from 'react'
import './News.css'
import b from '../../assets/images/b.jpg'
const News = () => {
    return (
    <section className="news">
    <div className="container">
        <div className="row valign-wrapper">
            <div className="col l6 m6">
                <h2 className="news__subtitle">Просмотрите наши</h2>
                <h2 className="news__title">Последние новости и события</h2>
            </div>
            <hr className="col l3 m3" />
            <div className="col l3 m3 right-align">
                <a href="#">ПРОСМОТРЕТЬ ПОЛНОСТЬЮ</a>
            </div>
        </div>
        <div className="row">
            <div className="col m3 l3">
                <img className="card__image" src={b} alt="" />
                <h2 className="card__title">Название события</h2>
                <span className="card__author">Автор статьи</span>
                <span className="card__date">Дата</span>
                <p className="card__text">Lorem ipsum dolor sit amet consectetur adipisicing elit. Veniam repudiandae sequi sint saepe iste</p>
                <a className="card__link">Читать далее</a>
            </div>
            <div className="col m3 l3">
                <img className="card__image" src={b} alt="" />
                <h2 className="card__title">Название события</h2>
                <span className="card__author">Автор статьи</span>
                <span className="card__date">Дата</span>
                <p className="card__text">Lorem ipsum dolor sit amet consectetur adipisicing elit. Veniam repudiandae sequi sint saepe iste</p>
                <a className="card__link">Читать далее</a>
            </div>
            <div className="col m3 l3">
                <img className="card__image" src={b} alt="" />
                <h2 className="card__title">Название события</h2>
                <span className="card__author">Автор статьи</span>
                <span className="card__date">Дата</span>
                <p className="card__text">Lorem ipsum dolor sit amet consectetur adipisicing elit. Veniam repudiandae sequi sint saepe iste</p>
                <a className="card__link">Читать далее</a>
            </div>
            <div className="col m3 l3">
                <img className="card__image" src={b} alt="" />
                <h2 className="card__title">Название события</h2>
                <span className="card__author">Автор статьи</span>
                <span className="card__date">Дата</span>
                <p className="card__text">Lorem ipsum dolor sit amet consectetur adipisicing elit. Veniam repudiandae sequi sint saepe iste</p>
                <a className="card__link">Читать далее</a>
            </div>
        </div>
    </div>
</section>
    )
}

export default News
