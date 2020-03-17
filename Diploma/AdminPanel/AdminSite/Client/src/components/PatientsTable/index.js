import React from 'react'
import { Table } from 'components' 
const PatientsTable = () => {
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
                accessor: "Email"
              },
              {
                Header: "Mobile",
                accessor: "Mobile"
              },
              {
                Header: "Cardiogram",
                accessor: "Cardiogram"
              }
            ]
          }
        ],
        []
    );
    const data = React.useMemo(
        () => [
            {
                firstName: "nation",
                Email: "election",
                age: 20,
                Email: 47,
                Mobile: 85,
                Cardiogram: <button>123123</button>,
                status: "complicated",
                subRows: undefined
            },
            {
                firstName: "asdasd",
                Email: "election",
                age: 20,
                Email: 47,
                Mobile: 85,
                status: "complicated",
                subRows: undefined
            },
            {
                firstName: "asdasd",
                Email: "election",
                age: 20,
                Email: 47,
                Mobile: 85,
                status: "complicated",
                subRows: undefined
            },
            {
                firstName: "asdasd",
                Email: "election",
                age: 20,
                Email: 47,
                Mobile: 85,
                status: "complicated",
                subRows: undefined
            },
        ],
    []
    )
    return (
        <>
            <Table columns={columns} data={data}/>   
        </>
    )
}

export default PatientsTable
