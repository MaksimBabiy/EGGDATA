import React from 'react'
import { Link } from 'react-router-dom';
const DoctorItem = ({name, desc, _id='1'}) => {
    return (
        <div class="col s12 m4">
              <div class="card">
                  <div class="card-image">
                      <img src="../../../assets/images/c.jpg" />
                  </div>
                  <div class="card-content">
                      <span class="card-title">Максим Иванов</span>
                      <p>I am a very simple card. I am good at containing small bits of information.
                    I am convenient because I require little markup to use effectively.</p>
                   </div>
                  <div class="card-action">
                      <Link class="purple-text accent-4" to={`/doctors/${_id}`}>Узнать больше</Link>
                  </div>
              </div>
          </div>
    )
}
export default DoctorItem
