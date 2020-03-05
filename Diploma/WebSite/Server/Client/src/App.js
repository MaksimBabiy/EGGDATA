import React from 'react';
import { Header, Adventages,Registration, Doctors, News,FAQ,Footer, SliderCarousel } from 'components';

const App = () => {
  return (
    <div className="App">
      <Header />
      <SliderCarousel />
      <Adventages />
      <Registration />
      <Doctors />
      <News />
      <FAQ />
      <Footer />
    </div>
  );
}

export default App;
