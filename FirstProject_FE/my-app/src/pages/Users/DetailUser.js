import axios from "axios";
import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
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
  console.log("DetailUser ~ user", user);
  const navigate = useNavigate();
  const { email } = useParams();
  console.log("DetailUser ~ email", email);
  const {
    register,
    handleSubmit,
    formState: { errors, isValid },
  } = useForm({
    resolver: yupResolver(schemaValidation),
  });
  async function getData() {
    let reponse = await axios
      .get(`https://localhost:7123/api/User/${email}`)
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
  //console.log("phone", user.phoneNumber);
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
      {/* <div className="d-flex ">
        <button className="p-2 ms-auto bg-primary text-light" type="">
          Thay đổi thông tin
        </button>
      </div> */}
    </form>
  );
};

export default DetailUser;
