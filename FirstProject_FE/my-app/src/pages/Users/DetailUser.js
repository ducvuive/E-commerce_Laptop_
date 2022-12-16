import axios from "axios";
import React, { useEffect, useState } from "react";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import { useForm } from "react-hook-form";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
const schemaValidation = yup.object({
  tenDM: yup
    .string()
    .required("Vui lòng nhập danh mục")
    .max(50, "Danh mục có dưới 50 kí tự"),
});

const DetailUser = () => {
  const [user, setUser] = useState("");
  const location = useLocation();
  const emailUser = location.state.email;
  const {
    register,
    handleSubmit,
    formState: { errors, isValid },
  } = useForm({
    resolver: yupResolver(schemaValidation),
  });
  async function getData() {
    let reponse = await axios
      .get(`https://localhost:7123/api/User/${emailUser}`)
      .then((response) => {
        setUser(response.data);
      });
  }
  useEffect(() => {
    getData();
  }, []);
  const onSubmit = (data) => {
    if (isValid) {
      console.log(123);
    }
  };
  return (
    <form className="_form " onSubmit={handleSubmit(onSubmit)}>
      <div className="d-flex justify-content-center">
        <h3>Thông tin người dùng</h3>
      </div>
      <div className="mb-2 d-flex flex-column">
        <label htmlFor="">UserName</label>
        <input
          type="text"
          value={user.userName}
          className="form-control"
          disabled
        />
      </div>
      <div className="mb-2 d-flex flex-column">
        <label htmlFor="">Email</label>
        <input
          type="text"
          value={user.email}
          className="form-control"
          disabled
        />
      </div>
      <div className="mb-2 d-flex flex-column">
        <label htmlFor="">Số điện thoại</label>
        {user.phoneNumber ? (
          <input
            type="text"
            value={"0" + user.phoneNumber}
            className="form-control"
            disabled
          />
        ) : (
          <input type="text" value={user.phoneNumber} className="" disabled />
        )}
      </div>
    </form>
  );
};

export default DetailUser;
