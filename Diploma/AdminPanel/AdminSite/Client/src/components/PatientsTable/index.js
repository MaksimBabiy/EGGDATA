import React, { useEffect,useState } from 'react'
import { Table } from 'components';
import { Button,Modal, Input } from 'antd'
import './patienttable.scss'
import { connect } from 'react-redux'
import { userActions } from 'redux/actions' 
import { patientApi } from 'utils/api'
const PatientsTable = ({tableValue,patientId}) => {
  const [data,setData] = useState()
  const [isVisiable, setIsVisiable] = useState(false)
  const [isEditVisiable, setIsEditVisiable] = useState(false)
  const [inputValue, setInputValue] = useState({})
  const [editValue, setEditValue] = useState()
  useEffect(() => {
    patientApi.get().then(({data}) => setData(data))
  },[isVisiable, isEditVisiable])
  useEffect(() => {
    setEditValue(tableValue)
  },[])
  const handleChangeInput = (e) => {
    setInputValue({
      ...inputValue,
      [e.target.name]: e.target.value
    })
  }
  const handleChangeEditInput = e => {
    setEditValue({
      ...editValue,
      [e.target.name]: e.target.value
    })
  }
    const columns = React.useMemo(
        () => [
          {
            Header: "PatientTable",
            columns: [
              {
                Header: "Full Name",
                accessor: "firstName"
              },
              {
                Header: "Age",
                accessor: "age"
              },
              {
                Header: "Email",
                accessor: "email"
              },
              {
                Header: "Mobile",
                accessor: "phoneNumber"
              },
              {
                Header: "Cardiogram",
                accessor: "cardiogram"
              }
            ]
          }
        ],
        []
    );
    let arr = []
    data && data.forEach((item => {
     item.cardiogram = <button>Кардиограма</button>
     item.subRows = undefined
     arr.push(item)
    }))
    return (
      <div className="patients">
      <Button className="doctors__btn" onClick={() => setIsVisiable(!isVisiable)}>Добавление</Button>
      <Modal
        title="Добавление доктора"
        visible={isVisiable}
        onOk={() => {
         patientApi.add(inputValue).then((data) => console.log(data)).finally(() => setIsVisiable(false), setInputValue(''))
        }}
        onCancel={() => setIsVisiable(false)}
      >
        <Input placeholder="Фамилия" onChange={handleChangeInput} name="FirstName"/>
        <Input placeholder="Имя" onChange={handleChangeInput} name="LastName"/>
        <Input placeholder="Отчество" onChange={handleChangeInput} name="MiddleName"/>
        <Input placeholder="Возраст" onChange={handleChangeInput} name="Age"/>
        <Input placeholder="Вес" onChange={handleChangeInput} name="Weight"/>
        <Input placeholder="Рост" onChange={handleChangeInput} name="Height"/>
        <Input placeholder="Пол" onChange={handleChangeInput} name="Sex"/>
        <Input placeholder="Мобильный телефон" onChange={handleChangeInput} name="PhoneNumber"/>
        <Input placeholder="Домашний телефон" onChange={handleChangeInput} name="HomeNumber"/>
        <Input placeholder="E-mail" onChange={handleChangeInput} name="Email"/>
        <Input placeholder="О докторе" onChange={handleChangeInput} name="Condition"/>
      </Modal>
      {editValue && <Modal
        title="Редактирование доктора"
        visible={isEditVisiable}
        onOk={() => {
         patientApi.update(editValue).finally(() => {
          localStorage.setItem('value', JSON.stringify(editValue))
          setIsEditVisiable(false)
          setInputValue('')
         })
        }}
        onCancel={() => setIsEditVisiable(false)}
      >
        <Input placeholder="Фамилия" value={editValue.firstName} onChange={handleChangeEditInput} name="firstName"/>
        <Input placeholder="Имя" value={editValue.lastName} onChange={handleChangeEditInput} name="lastName"/>
        <Input placeholder="Отчество" value={editValue.middleName} onChange={handleChangeEditInput} name="middleName"/>
        <Input placeholder="Возраст" value={editValue.age} onChange={handleChangeEditInput} name="age"/>
        <Input placeholder="Вес" value={editValue.weight} onChange={handleChangeEditInput} name="weight"/>
        <Input placeholder="Рост" value={editValue.height} onChange={handleChangeEditInput} name="height"/>
        <Input placeholder="Пол" value={editValue.sex} onChange={handleChangeEditInput} name="sex"/>
        <Input placeholder="Мобильный телефон" value={editValue.phoneNumber} onChange={handleChangeEditInput} name="phoneNumber"/>
        <Input placeholder="Домашний телефон" value={editValue.homeNumber} onChange={handleChangeEditInput} name="homeNumber"/>
        <Input placeholder="E-mail" value={editValue.email} onChange={handleChangeEditInput} name="email"/>
        <Input placeholder="О докторе" value={editValue.condition} onChange={handleChangeEditInput} name="condition"/>
        <Button onClick={() => patientApi.delete(patientId).finally(() => setIsEditVisiable(false))}>Удалить доктора</Button>
      </Modal>}
      <Table columns={columns} data={arr} isEditVisiable={isEditVisiable} setIsEditVisiable={setIsEditVisiable} setEditValue={setEditValue}/>     
      </div>
    )
}

export default connect(({user}) => ({
  tableValue: user.tableValue,
  patientId: user.id
}), userActions)(PatientsTable)
