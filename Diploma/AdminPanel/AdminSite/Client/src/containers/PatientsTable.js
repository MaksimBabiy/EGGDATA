import React, { useEffect,useState } from 'react'
import { PatientsTable as BasePatientsTable } from 'components'
import { connect } from 'react-redux'
import { userActions } from 'redux/actions' 
import { patientApi } from 'utils/api'
import { Button } from 'antd'

const PatientsTable = ({tableValue,patientId}) => {
    const [data,setData] = useState()
    const [graphData, setGraphData] = useState()
    const [isVisiable, setIsVisiable] = useState(false)
    const [isEditVisiable, setIsEditVisiable] = useState(false)
    const [isVisiableGraph, setIsVisiableGraph] = useState(false)
    const [isLoading, setIsLoading] = useState(false)
    const [inputValue, setInputValue] = useState({
       cardiogram: null
    })
    const [editValue, setEditValue] = useState()
    const [isDisible, setIsDisible] = useState(true)
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
        patientApi.add(inputValue).then((data) => console.log(data)).finally(() => setIsVisiable(false),setInputValue(''))
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
              Header: "Таблиця пацієнтів",
              columns: [
                {
                  Header: "Ім'я",
                  accessor: "firstName", 
                },
                {
                  Header: "Прізвище",
                  accessor: "lastName", 
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
                  Header: "Електронна пошта",
                  accessor: "email"
                },
                {
                  Header: "Телефон",
                  accessor: "phoneNumber"
                },
                {
                  Header: "Кардіограма",
                  accessor: "cardiogram"
                }
              ]
            }
          ],
          []
      );

      const handleGetGraph = (id) => {
       patientApi.getGraph(id).then(({data}) => setGraphData(data), console.log(data)).finally(() => {
        setIsVisiableGraph(!isVisiableGraph)
        setIsLoading(false)
       })
      }
      const handleDisible = (patientId) => {
        setIsDisible({
          ...isDisible,
          [patientId]: false
        })
      }
      let arr = []
      
      let newArr = Object.keys(isDisible).map(item => Number(item))
      data && data.forEach((item => {
       
       item.cardiogram = <Button onClick={()=> {
         setIsLoading(true)
         handleGetGraph(item.patientId)
        }} style={{zIndex: 99999999}} disabled={newArr.includes(item.patientId) && isDisible[patientId] !== undefined ? false : true}>Отобразить</Button>
       item.subRows = undefined
       arr.push(item)
      }))
      const handleUpload = (e) => {
        const data = new FormData() 
        data.append('file', e.target.files[0])
        patientApi.uploadFile(data,patientId).then(() => handleDisible(patientId))
      }
      // const props = {
      //   name: 'file',
      //   action: 'https://www.mocky.io/v2/5cc8019d300000980a055e76',
      //   headers: {
      //     authorization: 'authorization-text',
      //   },
      //   onChange(info) {
      //     if (info.file.status === 'done') {
      //       message.success(`${info.file.name} file uploaded successfully`);
      //       let formData = new FormData()
      //       const file = new File([info.file], 'file.dat')
      //       formData.append('file', file)
      //       console.log(info)
      //       patientApi.uploadFile(formData,patientId).then((data) => console.log(data))
      //     } else if (info.file.status === 'error') {
      //       message.error(`${info.file.name} file upload failed.`);
      //     }
        
      //   },
      // };
    return <BasePatientsTable 
    data={arr}
    graphData={graphData}
    isVisiableGraph={isVisiableGraph}
    setIsVisiableGraph={setIsVisiableGraph}
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
    setInputValue={setInputValue}
    setIsEditVisiable={setIsEditVisiable}
    setIsVisiable={setIsVisiable}
    // props={props}
    handleUpload={handleUpload}
    isLoading={isLoading}
    />
}

export default connect(({user}) => ({
    tableValue: user.tableValue,
    patientId: user.id
  }), userActions)(PatientsTable)
  
