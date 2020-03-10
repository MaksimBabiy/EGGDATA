import React from 'react'
import { Header, Adventages,Registration, Doctors, News,FAQ,Footer, SliderCarousel } from 'components';
const Home = () => {
    return (
    <div className="App">
      <SliderCarousel />
      <Adventages />
      <Registration />
      <Doctors />
      <News />
      <FAQ />
    </div>
    )
}

export default Home
