import React, { Fragment, useEffect, useState } from "react";
import axios from "axios";
import Table from "react-bootstrap/Table";
import { Link } from "react-router-dom";
import { Button, Grid } from "@mui/material";
import { DataGrid } from "@mui/x-data-grid";
const ListProduct = () => {
  const [products, setProducts] = useState([]);
  console.log("ListProduct ~ products", products);
  const loadCate = async () => {
    await axios
      .get("https://localhost:7123/api/SanPhams/all")
      .then((response) => {
        setProducts(response.data);
        //console.log(response.data);
      });
  };
  useEffect(() => {
    loadCate();
  }, []);

  function DeleteProduct(id) {
    console.log("DeleteCate ~ id", id);
    axios
      .delete(`https://localhost:7123/api/SanPhams/${id}`)
      .then(setProducts(products.filter((o, i) => o.sanPhamId !== id)));
  }

  const columns = [
    { field: "sanPhamId", flex: 1, headerName: "ID", type: "number" },
    { field: "tenSP", flex: 1, headerName: "Tên sản phẩm" },
    { field: "soLuong", flex: 1, headerName: "Số lượng" },
    { field: "donGia", flex: 1, headerName: "Giá" },
    {
      field: "action",
      headerName: "Tác vụ",
      flex: 1,
      renderCell: ({ row: { sanPhamId } }) => {
        return (
          <Grid>
            <Link to={`/listProduct/Detail/${sanPhamId}`}>
              <Button variant="outlined">Chi tiết</Button>
            </Link>
            <Button
              variant="outlined"
              color="error"
              className="ms-3"
              onClick={() => DeleteProduct(sanPhamId)}
            >
              Xóa
            </Button>
            {/* </Link> */}
          </Grid>
        );
      },
    },
  ];

  return (
    <Fragment>
      <Link
        to={`Create`}
        className="px-3 py-2 mb-2 d-inline-block button_action bg-primary"
      >
        Tạo sản phẩm
      </Link>
      <div style={{ height: "79vh", width: "85vw" }}>
        <DataGrid
          rows={products}
          columns={columns}
          pageSize={8}
          rowsPerPageOptions={[5]}
          getRowId={(row) => row.sanPhamId}
        />
      </div>
    </Fragment>
  );
};

export default ListProduct;
