import React from 'react'
import AwesomeSlider from 'react-awesome-slider';
import 'react-awesome-slider/dist/styles.css';
import semen from './Slider.scss'
import a from '../../assets/images/a.jpg'
import b from '../../assets/images/b.jpg'
import c from '../../assets/images/c.jpg'
import d from '../../assets/images/d.jpg'
import e from '../../assets/images/e.jpg'

const SliderCarousel = () => {
    return (
      <AwesomeSlider
      cssModule={semen}
      >
      <p data-src={a} >This is our big Tagline!
Here's our small slogan.</p>
      <p data-src={b} >Right Aligned Caption
Here's our small slogan.</p>
      <p data-src={c} >Left Aligned Caption
Here's our small slogan.</p>
      <p data-src={d} >This is our big Tagline!
Here's our small slogan.</p>
      <p data-src={e} >Right Aligned Caption
Here's our small slogan.</p>
    </AwesomeSlider>
    )
}

export default SliderCarousel
