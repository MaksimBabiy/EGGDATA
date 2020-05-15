import React, { useEffect,useState } from 'react'
import { PatientsTable as BasePatientsTable } from 'components'
import { connect } from 'react-redux'
import { userActions } from 'redux/actions' 
import { patientApi } from 'utils/api'
import { message  } from 'antd'
const PatientsTable = ({tableValue,patientId}) => {
    const [data,setData] = useState()
    const [graphData, setGraphData] = useState()
    const [isVisiable, setIsVisiable] = useState(false)
    const [isEditVisiable, setIsEditVisiable] = useState(false)
    const [isVisiableGraph, setIsVisiableGraph] = useState(false)
    const [inputValue, setInputValue] = useState({
       cardiogram: null
    })
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

      const handleGetGraph = (id) => {
       patientApi.getGraph(id).then(({data}) => setGraphData(data)).finally(() => setIsVisiableGraph(!isVisiableGraph))
      }

      let arr = []
      data && data.forEach((item => {
    
       item.cardiogram = <button onClick={()=> handleGetGraph(item.patientId)} style={{zIndex: 99999999}}>Кардиограма</button>
       item.subRows = undefined
       arr.push(item)
      }))
      const props = {
        name: 'file',
        action: 'https://www.mocky.io/v2/5cc8019d300000980a055e76',
        headers: {
          authorization: 'authorization-text',
        },
        onChange(info) {
          if (info.file.status === 'done') {
           
            message.success(`${info.file.name} file uploaded successfully`);
            let formData = new FormData()
            const file = new File([info.file], 'file.dat')
            formData.append('file', file)
      
            patientApi.uploadFile(formData).then((data) => console.log(data))
          } else if (info.file.status === 'error') {
            message.error(`${info.file.name} file upload failed.`);
          }
        
        },
      };
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
    props={props}
    />
}

export default connect(({user}) => ({
    tableValue: user.tableValue,
    patientId: user.id
  }), userActions)(PatientsTable)
  
