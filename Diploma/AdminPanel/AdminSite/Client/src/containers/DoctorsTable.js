import React,{useEffect, useState} from 'react'
import { DoctorsTable as BaseDoctorsTable} from 'components'
import { connect } from 'react-redux'
import { userActions } from 'redux/actions'
import { doctorApi } from 'utils/api'
import  openDialogs  from '../utils/openDialogs'
const DoctorsTable = ({tableValue, doctorId}) => {
  const [data,setData] = useState()
  const [isVisiable, setIsVisiable] = useState(false)
  const [isEditVisiable, setIsEditVisiable] = useState(false)
  const [inputValue, setInputValue] = useState({})
  const [editValue, setEditValue] = useState()

  useEffect(() => {
    doctorApi.get().then(({data}) => setData(data))
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
    const handleAdd = () => {
        doctorApi.add(inputValue).then(({data}) => openDialogs({
          title: 'Информация',
          text: data, 
          type: "info"
        })).finally(() => setIsVisiable(false), setInputValue(''))
    }
    const handleUpdate = () => {
        doctorApi.update(editValue).finally(() => {
            localStorage.setItem('value', JSON.stringify(editValue))
            setIsEditVisiable(false)
            setInputValue('')
           })
    }
    const handleDelete = () => {
        doctorApi.delete(doctorId).finally(() => setIsEditVisiable(false))
    }
    const columns = React.useMemo(
        () => [
          {
            Header: "Таблиця докторів",
            columns: [
              {
                Header: "Ім'я",
                accessor: "lastName", 
              },
              {
                Header: "Прізвище",
                accessor: "firstName", 
              },
              {
                Header: "По батькові",
                accessor: "middleName", 
              },
              {
                Header: "Вік",
                accessor: "age"
              },
             
              {
                Header: "Телефон",
                accessor: "phoneNumber"
              },
              {
                Header: "Спеціальність",
                accessor: "condition"
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
    return <BaseDoctorsTable 
    arr={arr} 
    columns={columns} 
    handleAdd={handleAdd}
    handleUpdate={handleUpdate}
    handleDelete={handleDelete}
    doctorId={doctorId} 
    handleChangeEditInput={handleChangeEditInput} 
    handleChangeInput={handleChangeInput} 
    isVisiable={isVisiable} 
    setIsVisiable={setIsVisiable}
    isEditVisiable={isEditVisiable}
    setIsEditVisiable={setIsEditVisiable}
    inputValue={inputValue}
    editValue={editValue}
    setInputValue={setInputValue}
    setEditValue={setEditValue}
    handleUpdate={handleUpdate}
    />
}
export default connect(({user}) => ({
    tableValue: user.tableValue,
    doctorId: user.id
  }), userActions)(DoctorsTable)
