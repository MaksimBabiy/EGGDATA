import React from 'react'
import { Table } from 'components';
import { Button,Modal, Input } from 'antd'
import './doctorstable.scss'

const DoctorsTable = ({
  arr, 
  columns,
  handleChangeEditInput,
  handleChangeInput,
  isVisiable,
  setIsVisiable,
  isEditVisiable,
  setIsEditVisiable,
  inputValue,
  editValue,
  setInputValue,
  setEditValue,
  handleAdd,
  handleUpdate,
  handleDelete
}) => {
 
    return (
        <div className="doctors">
        <Button className="doctors__btn" onClick={() => setIsVisiable(!isVisiable)}>Добавить Доктора</Button>
        <Modal
          title="Добавление доктора"
          visible={isVisiable}
          onOk={handleAdd}
          onCancel={() => setIsVisiable(false)}
        >
          <Input placeholder="Фамилия" onChange={handleChangeInput} name="FirstName" value={inputValue.FirstName}/>
          <Input placeholder="Имя" onChange={handleChangeInput} name="LastName" value={inputValue.LastName}/>
          <Input placeholder="Отчество" onChange={handleChangeInput} name="MiddleName" value={inputValue.MiddleName}/>
          <Input placeholder="Возраст" onChange={handleChangeInput} name="Age" value={inputValue.Age}/>
          <Input placeholder="Вес" onChange={handleChangeInput} name="Weight" value={inputValue.Weight}/>
          <Input placeholder="Рост" onChange={handleChangeInput} name="Height"value={inputValue.Height}/>
          <Input placeholder="Пол" onChange={handleChangeInput} name="Sex" value={inputValue.Sex}/>
          <Input placeholder="Мобильный телефон" onChange={handleChangeInput} name="PhoneNumber" value={inputValue.PhoneNumber}/>
          <Input placeholder="Домашний телефон" onChange={handleChangeInput} name="HomeNumber" value={inputValue.HomeNumber}/>
          <Input placeholder="E-mail" onChange={handleChangeInput} name="Email" value={inputValue.Email}/>
          <Input placeholder="О докторе" onChange={handleChangeInput} name="Condition" value={inputValue.Condition}/>
        </Modal>
        {editValue && <Modal
          title="Редактирование доктора"
          visible={isEditVisiable}
          onOk={handleUpdate}
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
          <Button onClick={handleDelete}>Удалить доктора</Button>
        </Modal>}
        <Table columns={columns} data={arr} isEditVisiable={isEditVisiable} setIsEditVisiable={setIsEditVisiable} setEditValue={setEditValue}/>     
        </div>
    )
}
export default DoctorsTable
