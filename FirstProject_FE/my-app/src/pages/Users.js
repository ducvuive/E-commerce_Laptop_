import React, { useEffect, useState } from "react";
import axios from "axios";
import Table from "react-bootstrap/Table";
import { Link, useNavigate } from "react-router-dom";
import { Button, Grid } from "@mui/material";
import { DataGrid } from "@mui/x-data-grid";
import { Cookies } from "react-cookie";
const Users = () => {
  const [customers, setCustomers] = useState([]);
  const navigate = useNavigate();
  const loadCate = () => {
    const cookies = new Cookies();
    const token = cookies.get("token");
    axios
      .get("https://localhost:7123/api/User", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then((response) => {
        setCustomers(response.data);
      });
  };
  useEffect(() => {
    loadCate();
  }, []);
  const InfoUser = (email) => {
    navigate(`detail-user`, {
      state: {
        email: email,
      },
    });
  };
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
            <Button variant="outlined" onClick={() => InfoUser(email)}>
              Chi tiết
            </Button>
          </Grid>
        );
      },
    },
  ];

  return (
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
