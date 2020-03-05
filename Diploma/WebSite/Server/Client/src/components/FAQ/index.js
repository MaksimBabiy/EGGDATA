import React from 'react'

import levayastrelochka from '../../assets/images/levayastrelochka.png'
import pravayastrelochka from '../../assets/images/pravayastrelochka.png'
import './FAQ.css'
const FAQ = () => {
    return (
<section className="faq blue-grey darken-4">
    <div className="container">
        <div className="faq__content center-align white">
            <h4>Ответы на</h4>
            <h3>Часто задаваемые вопросы</h3>
            <hr />
            <div className="row center-align valign-wrapper">
                <div className="col m1">
                    <img src={levayastrelochka} alt="" className="prev-q" />
                </div>
                <p className="col m10">Lorem ipsum dolor sit amet consectetur, adipisicing elit. Ipsam officiis ducimus illo nesciunt corrupti, repellendus soluta quis excepturi! Mollitia nostrum aspernatur blanditiis hic quae. Exercitationem libero sunt nobis quos accusantium.</p>
                <div className="col m1">
                    <img src={pravayastrelochka} alt="" className="next-q" />
                </div>
            </div>
        </div> 
    </div>
</section>
    )
}

export default FAQ
