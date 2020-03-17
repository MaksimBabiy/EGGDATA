import React from 'react'
import comfort from '../../assets/images/comfort.png'
import './Adventages.css'
const Adventages = () => {
    
    return (
    <section className="advantages">
    
    <div className="container">
        <div className="row valign-wrapper">
            <div className="col l3 left-align">
                <h2 className="advantages__title">О Нас</h2>
            </div>
            <hr className="col l8 m6" />
            <div className="col l1 right-align">
                <a href="#" className="advantages__next">читать далее</a>
            </div>
        </div>

        <div className="row">
            <div className="col l3 m6 s12">
                <img className="advantage__img" src={comfort} /> 
                <h3 className="advantages__card--title">УДОБСТВО</h3>
                <p className="advantages__info">Lorem ipsum dolor sit amet consectetur adipisicing elit. Debitis possimus, eligendi a veritatis odio optio deleniti.</p>
            </div>
            <div className="col l3 m6 s12">
                <img className="advantage__img" src={comfort}   /> 
                <h3 className="advantages__card--title">АКТУАЛЬНОСТЬ</h3>
                <p className="advantages__info">Lorem ipsum dolor sit amet consectetur adipisicing elit. Debitis possimus, eligendi a veritatis odio optio deleniti.</p>
            </div>
            <div className="col l3 m6 s12">
                <img  className="advantage__img" src={comfort}   />    
                <h3 className="advantages__card--title">ДИАГНОСТИКА</h3> 
                <p className="advantages__info">Lorem ipsum dolor sit amet consectetur adipisicing elit. Debitis possimus, eligendi a veritatis odio optio deleniti.</p>
            </div>
            <div className="col l3 m6 s12">
               <img  className="advantage__img" src={comfort}   />   
               <h3 className="advantages__card--title">АНАЛИЗ</h3>  
               <p className="advantages__info">Lorem ipsum dolor sit amet consectetur adipisicing elit. Debitis possimus, eligendi a veritatis odio optio deleniti.</p>
           </div>
       </div> 
       <div className="row">
        <div className="col l3 m6 s12">
            <img className="advantage__img" src={comfort}   /> 
            <h3 className="advantages__card--title">УДОБСТВО</h3>
            <p className="advantages__info">Lorem ipsum dolor sit amet consectetur adipisicing elit. Debitis possimus, eligendi a veritatis odio optio deleniti.</p>
        </div>
        <div className="col l3 m6 s12">
            <img className="advantage__img"  src={comfort}   /> 
            <h3 className="advantages__card--title">АКТУАЛЬНОСТЬ</h3>
            <p className="advantages__info">Lorem ipsum dolor sit amet consectetur adipisicing elit. Debitis possimus, eligendi a veritatis odio optio deleniti.</p>
        </div>
        <div className="col l3 m6 s12">
            <img className="advantage__img"  src={comfort}   />    
            <h3 className="advantages__card--title">ДИАГНОСТИКА</h3> 
            <p className="advantages__info">Lorem ipsum dolor sit amet consectetur adipisicing elit. Debitis possimus, eligendi a veritatis odio optio deleniti.</p>
        </div>
        <div className="col l3 m6 s12">
           <img className="advantage__img"  src={comfort}   />   
           <h3 className="advantages__card--title">АНАЛИЗ</h3>  
           <p className="advantages__info">Lorem ipsum dolor sit amet consectetur adipisicing elit. Debitis possimus, eligendi a veritatis odio optio deleniti.</p>
       </div>
   </div> 
</div>
</section>
    )
}

export default Adventages
