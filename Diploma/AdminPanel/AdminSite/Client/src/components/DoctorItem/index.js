import React, { useState } from 'react'
import {Button,DatePicker,Modal,Input } from 'antd'
import './DoctorItem.scss'
const DoctorItem = (props) => {

    const [isVisiable, setIsVisiable] = useState(false)
    const  [infoValue, setInfoValue] = useState({})
  
    const handleInfo = (e) => {
        console.log(e)
        setInfoValue({
            ...infoValue,
            [e.target.name] : e.target.value
        })
    }
    const handleDate = (date, dateString) => {
        setInfoValue({
            ...infoValue,
            'date' : dateString
        })
    }
    return (
        <div class="col s12 m4">
              <div class="card">
                  <div class="card-content">
                      <span class="card-title">{props.firstName} {props.lastName} {props.middleName}</span>
                      <p>{props.condition}</p>
                   </div>
                  <div class="card-action">
                  <Button type="primary" onClick={() => setIsVisiable(!isVisiable)}>
                    Записаться на прием
                  </Button>
                  <div className="sem">
                    <Modal
                        title="Modal"
                        visible={isVisiable}
                        onOk={() => setIsVisiable(!isVisiable)}
                        onCancel={() => setIsVisiable(!isVisiable)}
                        >
                     <Input placeholder="ФИО" onChange={(e) => handleInfo(e)} name="info" />
                     <DatePicker onChange={handleDate} />
                    </Modal>
                    </div>
                  </div>
              </div>
          </div>
    )
}
export default DoctorItem
