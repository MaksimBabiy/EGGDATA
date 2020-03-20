import React from 'react'
import { useTable, usePagination } from "react-table";
import { connect } from 'react-redux'
import { userActions } from 'redux/actions'
import './table.scss'
const Table = ({ columns, data,setIsEditVisiable, isEditVisiable,setEditValue,setId,setTableValue }) => {
    const {
        getTableProps,
        getTableBodyProps,
        headerGroups,
        prepareRow,
        page,
        canPreviousPage,
        canNextPage,
        pageOptions,
        pageCount,
        gotoPage,
        nextPage,
        previousPage,
        setPageSize,
        state: { pageIndex, pageSize }
      } = useTable(
        {
          columns,
          data,
          initialState: { pageIndex: 0 }
        },
        usePagination
      );
  
    return (
   <>
        <table {...getTableProps()}>
        <thead>
          {headerGroups.map(headerGroup => (
            <tr {...headerGroup.getHeaderGroupProps()}>
              {headerGroup.headers.map(column => (
                <th {...column.getHeaderProps()}>{column.render("Header")}</th>
              ))}
            </tr>
          ))}
        </thead>
        <tbody {...getTableBodyProps()}>
          {page.map((row, i) => {
            prepareRow(row);
            return (
              <tr {...row.getRowProps()} >
                {row.cells.map((cell,index) => {
                  return (
                    index < 4 ?  <td {...cell.getCellProps()}  onClick={() => {
                      setEditValue(row.original)
                      setIsEditVisiable(!isEditVisiable)
                      setId(row.original.patientId ? row.original.patientId : row.original.doctorId)
                      setTableValue(row.original)
                    }}>{cell.render("Cell")}</td>
                    :
                    <td {...cell.getCellProps()} >{cell.render("Cell")}</td>
                  );
                })}
              </tr>
            );
          })}
        </tbody>
      </table>
      <div className="pagination">
        <button onClick={() => gotoPage(0)} disabled={!canPreviousPage}>
          {"<<"}
        </button>{" "}
        <button onClick={() => previousPage()} disabled={!canPreviousPage}>
          {"<"}
        </button>{" "}
        <button onClick={() => nextPage()} disabled={!canNextPage}>
          {">"}
        </button>{" "}
        <button onClick={() => gotoPage(pageCount - 1)} disabled={!canNextPage}>
          {">>"}
        </button>{" "}
        <span>
          Page{" "}
          <strong>
            {pageIndex + 1} of {pageOptions.length}
          </strong>{" "}
        </span>
        <span>
          | Go to page:{" "}
          <input
            type="number"
            defaultValue={pageIndex + 1}
            onChange={e => {
              const page = e.target.value ? Number(e.target.value) - 1 : 0;
              gotoPage(page);
            }}
            style={{ width: "100px" }}
          />
        </span>{" "}
        <select
          value={pageSize}
          onChange={e => {
            setPageSize(Number(e.target.value));
          }}
        >
         
        </select>
      </div>
    </>
    )
}

export default connect(({data}) => data,userActions)(Table)
