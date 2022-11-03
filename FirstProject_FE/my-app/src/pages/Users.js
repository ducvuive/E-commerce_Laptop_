import React, { useEffect, useState } from "react";
import axios from "axios";
import Table from "react-bootstrap/Table";
import { Link } from "react-router-dom";
import Button from "react-bootstrap/Button";
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

  return (
    <div>
      <Table striped bordered className="text-center">
        <thead>
          <tr>
            <th>STT</th>
            <th>Email</th>
            <th>Tác vụ</th>
          </tr>
        </thead>
        <tbody>
          {customers.map((data, index) => (
            //console.log(data)
            <tr key={index}>
              <td>{index + 1}</td>
              <td>{data.email}</td>
              <td className="d-flex align-items-center justify-content-center">
                <Link
                  to={`Detail/${data.email}`}
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

export default Users;
