import React from 'react'

import c from '../../assets/images/c.jpg'
import './Doctors.css'
const Doctors = () => {
    return (
<section className="doctors">
    <div className="container">
        <div className="row valign-wrapper">
            <div className="col l6">
                <h2 className="doctors__subtitle">Наши</h2>
                <h2 className="doctors__title">квалифицированные доктора</h2>
            </div>
            <hr className="col l3 m6"/>
            <div className="col l3 right-align">
                <a href="#" className="record purple-text accent-4"> Записаться на приём</a>
            </div>
        </div>
        <div className="row">
            <div className="col s12 m4">
                <div className="card">
                    <div className="card-image">
                        <img src={c}/>
                    </div>
                    <div className="card-content">
                        <span className="card-title">Максим Иванов</span>
                        <p>I am a very simple card. I am good at containing small bits of information.
                        I am convenient because I require little markup to use effectively.</p>
                    </div>
                    <div className="card-action">
                        <a className="purple-text accent-4" href="#">Узнать больше</a>
                    </div>
                </div>
            </div>
            <div className="col s12 m4">
                <div className="card">
                    <div className="card-image">
                        <img src={c} />
                    </div>
                    <div className="card-content">
                        <span className="card-title">Екатерина Петрова</span>
                        <p>I am a very simple card. I am good at containing small bits of information.
                        I am convenient because I require little markup to use effectively.</p>
                    </div>
                    <div className="card-action">
                        <a className="purple-text accent-4" href="#">Узнать больше</a>
                    </div>
                </div>
            </div>
            <div className="col s12 m4">
                <div className="card">
                    <div className="card-image">
                        <img src={c} />
                    </div>
                    <div className="card-content">
                        <span className="card-title">Иван Сидоров</span>
                        <p>I am a very simple card. I am good at containing small bits of information.
                        I am convenient because I require little markup to use effectively.</p>
                    </div>
                    <div className="card-action">
                        <a className="purple-text accent-4" href="#">Узнать больше</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>
    )
}

export default Doctors
