import React, { useEffect, useState } from "react";
import axios from "axios";
import Table from "react-bootstrap/Table";
import { Link } from "react-router-dom";
import { Button, Grid } from "@mui/material";
import { DataGrid } from "@mui/x-data-grid";
const Users = () => {
  const [customers, setCustomers] = useState([]);
  const loadCate = () => {
    console.log(customers);
    axios.get("https://localhost:7123/api/User").then((response) => {
      setCustomers(response.data);
      console.log(response.data);
    });
  };
  useEffect(() => {
    console.log("use effect");
    loadCate();
    console.log(customers);
  }, []);

  const columns = [
    { field: "userName", flex: 1, headerName: "Username", type: "string" },
    {
      field: "phoneNumber",
      flex: 1,
      headerName: "Số điện thoại",
      type: "string",
    },
    { field: "email", flex: 1, headerName: "Email" },
    {
      field: "action",
      headerName: "Action",
      flex: 1,
      renderCell: ({ row: { email } }) => {
        return (
          <Grid>
            <Link to={`/users/Detail/${email}`}>
              <Button variant="outlined">Chi tiết</Button>
            </Link>
          </Grid>
        );
      },
    },
  ];

  return (
    // <div>
    //   <Table striped bordered className="text-center">
    //     <thead>
    //       <tr>
    //         <th>STT</th>
    //         <th>Email</th>
    //         <th>Tác vụ</th>
    //       </tr>
    //     </thead>
    //     <tbody>
    //       {customers.map((data, index) => (
    //         //console.log(data)
    //         <tr key={index}>
    //           <td>{index + 1}</td>
    //           <td>{data.email}</td>
    //           <td className="d-flex align-items-center justify-content-center">
    //             <Link
    //               to={`Detail/${data.email}`}
    //               className="px-6 py-2 button_action bg-info"
    //               variant="info"
    //             >
    //               Chi tiết
    //             </Link>
    //           </td>
    //         </tr>
    //       ))}
    //     </tbody>
    //   </Table>
    // </div>
    <div style={{ height: "85vh", width: "85vw" }}>
      <DataGrid
        rows={customers}
        columns={columns}
        pageSize={9}
        rowsPerPageOptions={[5]}
        getRowId={(row) => row.id}
      />
    </div>
  );
};

export default Users;
