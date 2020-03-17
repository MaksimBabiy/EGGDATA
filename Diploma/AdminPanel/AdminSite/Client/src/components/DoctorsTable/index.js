import React from 'react'
import { Table } from 'components';

const DoctorsTable = () => {
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
                accessor: "Email"
              },
              {
                Header: "Mobile",
                accessor: "Mobile"
              },
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

export default DoctorsTable
