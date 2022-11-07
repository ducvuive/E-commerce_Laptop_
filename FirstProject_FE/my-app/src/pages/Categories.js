import React, { useEffect, useState } from "react";
import axios from "axios";
import Table from "react-bootstrap/Table";
import { Link } from "react-router-dom";
import Button from "react-bootstrap/Button";
const Categories = () => {
  const [categories, setCategogies] = useState([]);
  //console.log("Categories ~ categories", categories);
  const loadCate = async () => {
    await axios
      .get("https://localhost:7123/api/DanhMucSanPhams")
      .then((response) => {
        setCategogies(response.data);
      });
  };
  useEffect(() => {
    console.log("use effect");
    loadCate();
  }, []);

  function DeleteCate(id) {
    console.log("DeleteCate ~ id", id);
    axios
      .delete(`https://localhost:7123/api/DanhMucSanPhams/${id}`)
      .then(setCategogies(categories.filter((o, i) => o.dmspId !== id)));
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
      {/* <h1>Category</h1> */}
      {/* <TableList link={link}></TableList> */}
      <Table striped bordered className="text-center">
        <thead>
          <tr>
            <th>STT</th>
            <th>Tên danh mục</th>
            <th>Tác vụ</th>
          </tr>
        </thead>
        <tbody>
          {categories.map((data, index) => (
            //console.log(data)
            <tr key={data.dmspId}>
              <td>{index + 1}</td>
              <td>{data.tenDM}</td>
              <td className="d-flex align-items-center justify-content-center">
                <Button
                  onClick={() => DeleteCate(data.dmspId)}
                  className="px-6 py-2 button_action bg-danger"
                  variant="info"
                >
                  Xóa
                </Button>{" "}
                {/* <Link
                  to={`Update/${data.dmspId}`}
                  className="px-6 py-2 button_action bg-info"
                  variant="info"
                >
                  Cập nhật
                </Link> */}
                <Link
                  to={`Detail/${data.dmspId}`}
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

export default Categories;
