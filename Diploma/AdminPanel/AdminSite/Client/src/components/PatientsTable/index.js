import React, {useEffect, useState} from 'react'
import { Table } from 'components' 
import axios from 'axios'
const PatientsTable = () => {
  const [data,setData] = useState()
  useEffect(() => {
    axios.get(`http://localhost:56839/api/AdminPatients/Get`).then(({data}) => setData(data))
  },[])
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
        <>
            <Table columns={columns} data={arr}/>   
        </>
    )
}

export default PatientsTable
