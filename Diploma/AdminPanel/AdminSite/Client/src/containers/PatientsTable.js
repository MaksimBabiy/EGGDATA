import React, { useEffect,useState } from 'react'
import { PatientsTable as BasePatientsTable } from 'components'
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
    const handleAdd = () => {
        patientApi.add(inputValue).then((data) => console.log(data)).finally(() => setIsVisiable(false), setInputValue(''))
    }
    const handleUpdate = () => {
        patientApi.update(editValue).finally(() => {
            localStorage.setItem('value', JSON.stringify(editValue))
            setIsEditVisiable(false)
            setInputValue('')
           })
    }
    const handleDelete = () => {
        patientApi.delete(patientId).finally(() => setIsEditVisiable(false))
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
    
    return <BasePatientsTable 
    data={arr}
    isVisiable={isVisiable}
    isEditVisiable={isEditVisiable}
    inputValue={inputValue}
    editValue={editValue}
    handleChangeEditInput={handleChangeEditInput}
    handleChangeInput={handleChangeInput}
    handleAdd={handleAdd}
    handleUpdate={handleUpdate}
    handleDelete={handleDelete}
    columns={columns}
    setEditValue={setEditValue}
    setIsEditVisiable={setIsEditVisiable}
    setIsVisiable={setIsVisiable}
    />
}

export default connect(({user}) => ({
    tableValue: user.tableValue,
    patientId: user.id
  }), userActions)(PatientsTable)
  
