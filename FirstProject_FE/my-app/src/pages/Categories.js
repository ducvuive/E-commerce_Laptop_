import React, { Fragment, useEffect, useState } from "react";
import axios from "axios";
import Table from "react-bootstrap/Table";
import { Link } from "react-router-dom";
import { Grid, Button } from "@mui/material";
import { DataGrid } from "@mui/x-data-grid";
import ConfirmAction from "../components/modal/Modal";
import { instanceOf } from "prop-types";
import { withCookies, Cookies } from "react-cookie";
const Categories = () => {
  const [categories, setCategogies] = useState([]);
  const [idCategories, setIdCategories] = useState("");
  console.log("Categories ~ idCategories", idCategories);
  const [modalShow, setModalShow] = React.useState(false);
  console.log("Categories ~ categories", categories);
  const loadCate = async () => {
    const cookies = new Cookies();
    console.log("cookie123", cookies.get("token"));
    const token = cookies.get("token");
    await axios
      .get("https://localhost:7123/api/DanhMucSanPhams", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then((response) => {
        setCategogies(response.data);
      });
  };
  useEffect(() => {
    console.log("use effect");
    loadCate();
  }, []);
  function ConfirmForm(id) {
    setModalShow(true);
    setIdCategories(id);
  }
  function DeleteCate() {
    console.log("DeleteCate ~ id", idCategories);
    setModalShow(false);
    axios
      .put(`https://localhost:7123/api/DanhMucSanPhams/delete/${idCategories}`)
      .then(
        setCategogies(categories.filter((o, i) => o.dmspId !== idCategories))
      );
    setIdCategories();
  }
  function HideModal() {
    setIdCategories();
    setModalShow(false);
  }
  const columns = [
    { field: "dmspId", flex: 1, headerName: "ID", type: "number" },
    { field: "tenDM", flex: 1, headerName: "Danh mục sản phẩm" },
    {
      field: "action",
      headerName: "Tác vụ",
      flex: 1,
      renderCell: ({ row: { dmspId } }) => {
        return (
          <Grid>
            <Link to={`/dashboard/Detail/${dmspId}`}>
              <Button variant="outlined">Chi tiết</Button>
            </Link>
            {/* <Link to={`/category/update/${dmspId}`} className="ms-3"> */}
            {/* <Button
              variant="outlined"
              color="error"
              className="ms-3"
              onClick={() => DeleteCate(dmspId)}
            >
              Xóa
            </Button> */}
            <Button
              variant="outlined"
              color="error"
              className="ms-3"
              onClick={() => ConfirmForm(dmspId)}
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
      <ConfirmAction
        title="Xác nhận xóa danh mục"
        content="Bạn có chắc chắn muốn xóa danh mục không ?"
        show={modalShow}
        onHide={() => HideModal()}
        onConfirm={() => DeleteCate()}
      ></ConfirmAction>
      <Link
        to={`Create`}
        className="px-3 py-2 mb-2 d-inline-block button_action bg-primary"
      >
        Tạo danh mục
      </Link>
      <div style={{ height: "79vh", width: "85vw" }}>
        <DataGrid
          rows={categories}
          columns={columns}
          pageSize={8}
          //rowsPerPageOptions={[5]}
          getRowId={(row) => row.dmspId}
        />
      </div>
    </Fragment>
  );
};

export default Categories;
