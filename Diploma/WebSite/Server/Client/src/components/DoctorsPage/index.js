import React,{useEffect} from 'react'
import { DoctorItem } from 'components'
import axios from 'axios'
const DoctorsPage = () => {
    useEffect(() => {
       axios.get('http://localhost:10847/api/Doctors/Get').then(({data}) => console.log(data))
    }, [])
    return (
   <section class="doctors">
    <div class="container">
    <div class="row valign-wrapper">
          <div class="col l6">
              <h2 class="doctors__subtitle">Наши</h2>
              <h2 class="doctors__title">квалифицированные доктора</h2>
          </div>
          <hr class="col l3 m6" />
          <div class="col l3 right-align">
              <a href="#" class="record purple-text accent-4"><i class="material-icons">date_range</i> Записаться на приём</a>
          </div>
    </div> 
    <div class="row">
        <DoctorItem />
    </div>
    </div>
    </section>
    )
}

export default DoctorsPage
