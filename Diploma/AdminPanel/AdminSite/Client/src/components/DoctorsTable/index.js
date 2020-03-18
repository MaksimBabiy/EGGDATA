import React, { useEffect,useState } from 'react'
import { Table } from 'components';
import axios from 'axios'

const DoctorsTable = () => {
  const [data,setData] = useState()
  useEffect(() => {
    axios.get(`http://localhost:56839/api/AdminDoctors/Get`).then(({data}) => setData(data))
  },[])
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
        <>
        <Table columns={columns} data={arr}/>     
        </>
    )
}

export default DoctorsTable
