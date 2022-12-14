import React, { Fragment, useEffect, useState } from "react";
import axios from "axios";
import Table from "react-bootstrap/Table";
import { useNavigate, Link } from "react-router-dom";
import {
  Button,
  Grid,
  Paper,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from "@mui/material";
import { DataGrid } from "@mui/x-data-grid";
import ConfirmAction from "../components/modal/Modal";
import { Cookies } from "react-cookie";
import PaginationComponent from "./AssignmentPagination";
const ListProduct = () => {
  const [products, setProducts] = useState([]);
  const [modalShow, setModalShow] = useState(false);
  const itemsPerPage = 6;
  const [idProduct, setIdProduct] = useState("");
  const [itemsNumber, setItemsNumber] = useState(itemsPerPage);
  const [currentPage, setCurrentPage] = useState(1);
  const [itemsCount, setItemsCount] = useState("");
  const [lastPage, setLastPage] = useState("");
  const navigate = useNavigate();
  const loadCate = async () => {
    await axios
      .get("https://localhost:7123/api/Product/GetProductWithPage/", {
        params: {
          page: currentPage === 0 ? 1 : currentPage,
          limit: itemsPerPage,
        },
      })
      .then((response) => {
        setItemsCount(response.data.totalItem);
        setProducts(response.data.products);
        setLastPage(response.data.lastPage);
      });
  };
  useEffect(() => {
    console.log("effect");
    loadCate();
  }, [currentPage]);

  function ConfirmForm(id) {
    setModalShow(true);
    setIdProduct(id);
  }
  function HideModal() {
    setIdProduct();
    setModalShow(false);
  }
  function DeleteProduct() {
    setModalShow(false);
    axios
      .put(`https://localhost:7123/api/Product/DeleteProduct/${idProduct}`)
      .then(() => {
        //setProducts(products.filter((o, i) => o.productId !== idProduct));
        if ((itemsCount - 1) % itemsPerPage === 0 && currentPage === lastPage) {
          setCurrentPage(currentPage - 1);
        } else {
          loadCate();
        }
      });
    setIdProduct();
  }
  const editProduct = (productId) => {
    navigate(`detail-product`, {
      state: {
        productId: productId,
      },
    });
  };
  function CompactText(text) {
    var result = "";
    if (text != null) {
      if (text.length <= 50) return text;
      else {
        for (var i = 0; i < 50; i++) {
          result = result.concat(text[i]);
        }
        return result + "...";
      }
    } else return result;
  }
  // const columns = [
  //   { field: "productId", flex: 1, headerName: "ID", type: "number" },
  //   { field: "nameProduct", flex: 1, headerName: "T??n s???n ph???m" },
  //   { field: "quantity", flex: 1, headerName: "S??? l?????ng" },
  //   { field: "price", flex: 1, headerName: "Gi??" },
  //   {
  //     field: "action",
  //     headerName: "T??c v???",
  //     flex: 1,
  //     renderCell: ({ row: { productId } }) => {
  //       return (
  //         <Grid>
  //           <Button variant="outlined" onClick={() => editProduct(productId)}>
  //             Chi ti???t
  //           </Button>
  //           <Button
  //             variant="outlined"
  //             color="error"
  //             className="ms-3"
  //             onClick={() => ConfirmForm(productId)}
  //           >
  //             X??a
  //           </Button>
  //         </Grid>
  //       );
  //     },
  //   },
  // ];

  return (
    <div>
      <Link
        to={`Create`}
        className="px-3 py-2 mb-2 d-inline-block button_action bg-primary"
      >
        T???o s???n ph???m
      </Link>
      <table class="table table-hover">
        <thead>
          <tr>
            <th scope="col">Id</th>
            <th scope="col">T??n s???n ph???m</th>
            <th scope="col">S??? l?????ng</th>
            <th scope="col">Gi??</th>
            <th scope="col">Thao t??c</th>
          </tr>
        </thead>
        <tbody>
          {products.map((item, index) => (
            <tr key={item.productId}>
              <th scope="row">{item.productId}</th>
              <td>{CompactText(item.nameProduct)}</td>
              <td>{item.quantity}</td>
              <td>{item.price}</td>
              <td>
                <Button
                  variant="outlined"
                  onClick={() => editProduct(item.productId)}
                >
                  Chi ti???t
                </Button>
                <Button
                  variant="outlined"
                  color="error"
                  className="ms-3"
                  onClick={() => ConfirmForm(item.productId)}
                >
                  X??a
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <div className="row">
        <PaginationComponent
          itemsCount={itemsCount}
          itemsPerPage={itemsPerPage}
          currentPage={currentPage}
          setCurrentPage={setCurrentPage}
          alwaysShown={true}
        />
      </div>
      <ConfirmAction
        title="X??c nh???n x??a s???n ph???m"
        content="B???n c?? ch???c ch???n mu???n x??a s???n ph???m kh??ng ?"
        show={modalShow}
        onHide={() => HideModal()}
        onConfirm={() => DeleteProduct()}
      ></ConfirmAction>
    </div>
    // <Fragment>
    //   <ConfirmAction
    //     title="X??c nh???n x??a s???n ph???m"
    //     content="B???n c?? ch???c ch???n mu???n x??a s???n ph???m kh??ng ?"
    //     show={modalShow}
    //     onHide={() => HideModal()}
    //     onConfirm={() => DeleteProduct()}
    //   ></ConfirmAction>
    //   <Link
    //     to={`Create`}
    //     className="px-3 py-2 mb-2 d-inline-block button_action bg-primary"
    //   >
    //     T???o s???n ph???m
    //   </Link>
    //   <div style={{ height: "79vh", width: "85vw" }}>
    //     <DataGrid
    //       rows={products}
    //       columns={columns}
    //       pageSize={8}
    //       rowsPerPageOptions={[5]}
    //       getRowId={(row) => row.productId}
    //     />
    //   </div>
    // </Fragment>
  );
};

export default ListProduct;
