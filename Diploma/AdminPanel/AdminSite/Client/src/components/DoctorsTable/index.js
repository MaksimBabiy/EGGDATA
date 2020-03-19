import React, { useEffect,useState } from 'react'
import { Table } from 'components';
import { Button,Modal, Input } from 'antd'
import './doctorstable.scss'
import { doctorApi } from 'utils/api'

const DoctorsTable = () => {
  const [data,setData] = useState()
  const [isVisiable, setIsVisiable] = useState(false)
  const [isEditVisiable, setIsEditVisiable] = useState(false)
  const [inputValue, setInputValue] = useState({})
  const [editValue, setEditValue] = useState()
  useEffect(() => {
    doctorApi.get().then(({data}) => setData(data))
  },[isVisiable, isEditVisiable])
  useEffect(() => {
    setEditValue(JSON.parse(localStorage.getItem('value')))
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
            Header: "DoctorTable",
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
            ]
          }
        ],
        []
    );
  let arr = []
   data && data.forEach((item => {
    item.subRows = undefined
    arr.push(item)
   }))
    return (
        <div className="doctors">
        <Button className="doctors__btn" onClick={() => setIsVisiable(!isVisiable)}>Добавить Доктора</Button>
        <Modal
          title="Добавление доктора"
          visible={isVisiable}
          onOk={() => {
           doctorApi.add(inputValue).then((data) => console.log(data)).finally(() => setIsVisiable(false), setInputValue(''))
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
           doctorApi.update(editValue).finally(() => {
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
          <Button onClick={() => doctorApi.delete(JSON.parse(localStorage.getItem('doctorId'))).finally(() => setIsEditVisiable(false))}>Удалить доктора</Button>
        </Modal>}
        <Table columns={columns} data={arr} isEditVisiable={isEditVisiable} setIsEditVisiable={setIsEditVisiable} setEditValue={setEditValue}/>     
        </div>
    )
}
export default DoctorsTable
