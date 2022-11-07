import React, { useEffect, useState } from "react";
import axios from "axios";
import Table from "react-bootstrap/Table";
import { Link } from "react-router-dom";
import Button from "react-bootstrap/Button";
const ListProduct = () => {
  const [customers, setCustomers] = useState([]);
  const loadCate = async () => {
    await axios
      .get("https://localhost:7123/api/SanPhams/all")
      .then((response) => {
        setCustomers(response.data);
        console.log(response.data);
      });
  };
  useEffect(() => {
    loadCate();
  }, []);

  function DeleteCate(id) {
    console.log("DeleteCate ~ id", id);
    axios
      .delete(`https://localhost:7123/api/SanPhams/${id}`)
      .then(setCustomers(customers.filter((o, i) => o.sanPhamId !== id)));
  }
  return (
    <div>
      <Link
        to={`Create`}
        className="px-5 py-4 mb-5 d-inline-block button_action bg-primary"
        variant="info"
      >
        Tạo mới
      </Link>
      <Table striped bordered className="text-center">
        <thead>
          <tr>
            <th>STT</th>
            <th>Tên sản phẩm</th>
            <th>Số lượng</th>
            <th>Đơn giá</th>
            <th>Tác vụ</th>
          </tr>
        </thead>
        <tbody>
          {customers.map((data, index) => (
            //console.log(data)
            <tr key={index}>
              <td>{index + 1}</td>
              <td>{data.tenSP}</td>
              <td>{data.soLuong}</td>
              <td>{data.donGia}</td>
              <td className="d-flex align-items-center justify-content-center">
                <Button
                  onClick={() => DeleteCate(data.sanPhamId)}
                  className="px-6 py-2 button_action bg-danger"
                  variant="info"
                >
                  Xóa
                </Button>
                <Link
                  to={`Detail/${data.sanPhamId}`}
                  className="px-6 py-2 button_action bg-info"
                  variant="info"
                >
                  Chi tiết
                </Link>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
    </div>
  );
};

export default ListProduct;
