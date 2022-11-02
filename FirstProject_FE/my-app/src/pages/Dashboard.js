import { Box, Typography, useTheme } from "@mui/material";
import { DataGrid } from "@mui/x-data-grid";
import { useEffect, useState } from "react";
import axios from "axios";

function Dashboard() {
  // const theme = useTheme();
  // const colors = tokens(theme.palette.mode);
  const [productData, setProductData] = useState([]);

  const columns = [
    { field: "dmspId", flex: 1, headerName: "ID", type: "number" },
    { field: "tenDM", flex: 1, headerName: "Name Catogory" },
  ];
  useEffect(() => {
    axios
      .get(`https://localhost:7123/api/DanhMucSanPhams`)
      .then((res) => {
        setProductData(res.data);
      })
      .catch((error) => console.log(error));
  }, []);

  return (
    <div style={{ height: "85vh", width: "85vw" }}>
      <DataGrid
        rows={productData}
        columns={columns}
        pageSize={5}
        rowsPerPageOptions={[5]}
        checkboxSelection
        getRowId={(row) => row.dmspId}
      />
    </div>
    //   <div>
    //     <DataGrid rows={productData} columns={columns} />
    //   </div>
    //  <Box m="20px">
    //      <Header title="Product" subTitle="Manage Product" />
    //     <Box
    //       m="40px 0 0 0"
    //       height="75vh"
    //       sx={{
    //         "& .MuiDataGrid-root": {
    //           border: "none",
    //         },
    //         "& .MuiDataGrid-cell": {
    //           borderBottom: "none",
    //         },
    //         "& .MuiDataGrid-columnHeaders": {
    //           backgroundColor: colors.blueAccent[700],
    //           borderBottom: "none",
    //         },
    //         "& .MuiDataGrid-virtualScroller": {
    //           backgroundColor: colors.primary[400],
    //         },
    //         "& .MuiDataGrid-footerContainer": {
    //           borderTop: "none",
    //           backgroundColor: colors.blueAccent[700],
    //         },
    //       }}
    //     >
    //     </Box>

    //   </Box>
  );
}

export default Dashboard;
