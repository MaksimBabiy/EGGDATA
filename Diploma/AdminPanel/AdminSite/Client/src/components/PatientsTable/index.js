import React from 'react'
import { Table,Graph } from 'components';
import { Button,Modal, Input, Upload } from 'antd'
import { UploadOutlined } from '@ant-design/icons';
import './patienttable.scss'
import { LoadingOutlined } from '@ant-design/icons';
const PatientsTable = ({
  handleAdd,
  handleUpdate,
  handleDelete,
  graphData,
  data,
  columns,
  isVisiable,
  setIsVisiableGraph,
  isEditVisiable,
  editValue,
  handleChangeEditInput,
  handleChangeInput,
  setIsVisiable,
  setIsEditVisiable,
  setEditValue,
  inputValue,
  props,
  isVisiableGraph,
  isLoading
}) => {
  console.log(isLoading)
  const mystyle = {
    fontSize: '60px',
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%,-50%)'
  };
    return (
      <>
      {isLoading && <LoadingOutlined style={mystyle}/>}
     {isVisiableGraph && <Graph graphData={graphData} isVisiableGraph={isVisiableGraph} setIsVisiableGraph={setIsVisiableGraph}/>}
      <div className="patients">
      <Button className="doctors__btn" onClick={() => setIsVisiable(!isVisiable)}>Добавление</Button>
      <Modal
        title="Добавление Пациента"
        visible={isVisiable}
        onOk={handleAdd}
        onCancel={() => setIsVisiable(false)}
      >
        <Input placeholder="Фамилия" onChange={handleChangeInput} name="FirstName" value={inputValue.FirstName}/>
        <Input placeholder="Имя" onChange={handleChangeInput} name="LastName" value={inputValue.LastName}/>
        <Input placeholder="Отчество" onChange={handleChangeInput} name="MiddleName" value={inputValue.MiddleName}/>
        <Input placeholder="Возраст" onChange={handleChangeInput} name="Age" value={inputValue.Age}/>
        <Input placeholder="Вес" onChange={handleChangeInput} name="Weight" value={inputValue.Weight}/>
        <Input placeholder="Рост" onChange={handleChangeInput} name="Height" value={inputValue.Height}/>
        <Input placeholder="Пол" onChange={handleChangeInput} name="Sex" value={inputValue.Sex}/>
        <Input placeholder="Мобильный телефон" onChange={handleChangeInput} name="PhoneNumber" value={inputValue.PhoneNumber}/>
        <Input placeholder="Домашний телефон" onChange={handleChangeInput} name="HomeNumber" value={inputValue.HomeNumber}/>
        <Input placeholder="E-mail" onChange={handleChangeInput} name="Email" value={inputValue.Email}/>
        <Input placeholder="О докторе" onChange={handleChangeInput} name="Condition" value={inputValue.Condition}/>
      </Modal>
      {editValue && <Modal
        title="Редагування пацієнта"
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
        <Upload {...props} >
          <Button>
            <UploadOutlined /> Выберите файл
          </Button>
        </Upload>
        <Button onClick={handleDelete}>Удалить доктора</Button>
      </Modal>}
      <Table columns={columns} data={data} isEditVisiable={isEditVisiable} setIsEditVisiable={setIsEditVisiable} setEditValue={setEditValue} />     
      </div>
      </>
    )
}

export default PatientsTable