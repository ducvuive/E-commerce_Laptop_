import React, { Fragment, useEffect, useState } from "react";
import axios from "axios";
import Table from "react-bootstrap/Table";
import { Link, useNavigate } from "react-router-dom";
import { Grid, Button } from "@mui/material";
import { DataGrid } from "@mui/x-data-grid";
import ConfirmAction from "../components/modal/Modal";
import { instanceOf } from "prop-types";
import { Cookies } from "react-cookie";
const Categories = () => {
  const [categories, setCategogies] = useState([]);
  const [idCategories, setIdCategories] = useState("");
  const navigate = useNavigate();
  const [modalShow, setModalShow] = React.useState(false);
  const loadCate = async () => {
    const cookies = new Cookies();
    const token = cookies.get("token");
    await axios
      .get("https://localhost:7123/api/Categories", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then((response) => {
        setCategogies(response.data);
      });
  };
  useEffect(() => {
    loadCate();
  }, []);
  function ConfirmForm(id) {
    setModalShow(true);
    setIdCategories(id);
  }
  function DeleteCate() {
    setModalShow(false);
    axios
      .put(`https://localhost:7123/api/Categories/delete/${idCategories}`)
      .then(
        setCategogies(
          categories.filter((o, i) => o.categoryId !== idCategories)
        )
      );
    setIdCategories();
  }
  function HideModal() {
    setIdCategories();
    setModalShow(false);
  }
  const DetailCategory = (idCategory) => {
    navigate(`detail-categogy`, {
      state: {
        idCategory: idCategory,
      },
    });
  };
  const columns = [
    { field: "categoryId", flex: 1, headerName: "ID", type: "number" },
    { field: "name", flex: 1, headerName: "Danh mục sản phẩm" },
    {
      field: "action",
      headerName: "Tác vụ",
      flex: 1,
      renderCell: ({ row: { categoryId } }) => {
        return (
          <Grid>
            <Button
              variant="outlined"
              onClick={() => DetailCategory(categoryId)}
            >
              Chi tiết
            </Button>
            <Button
              variant="outlined"
              color="error"
              className="ms-3"
              onClick={() => ConfirmForm(categoryId)}
            >
              Xóa
            </Button>
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
          getRowId={(row) => row.categoryId}
        />
      </div>
    </Fragment>
  );
};

export default Categories;
