import React from 'react'
import { Header, Adventages, Doctors, News,FAQ,Footer, SliderCarousel } from 'components';
const Home = () => {
    return (
    <div className="App">
      <SliderCarousel />
      <Adventages />
      <Doctors />
      <News />
      <FAQ />
    </div>
    )
}
export default Home
