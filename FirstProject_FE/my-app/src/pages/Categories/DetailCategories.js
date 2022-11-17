import axios from "axios";
import React, { Fragment, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useForm } from "react-hook-form";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import Button from "react-bootstrap/esm/Button";
const schemaValidation = yup.object({
  tenDM: yup
    .string()
    .required("Vui lòng nhập danh mục")
    .max(50, "Danh mục có dưới 50 kí tự"),
  description: yup
    .string()
    .required("Vui lòng nhập mô tả")
    .max(50, "Danh mục có dưới 50 kí tự"),
});

const DetailCategories = () => {
  const [categories, setCategogies] = useState("");
  console.log("DetailCategories ~ categories", categories);
  const navigate = useNavigate();
  const { id } = useParams();
  const {
    register,
    handleSubmit,
    setValue,
    formState: { errors, isValid },
  } = useForm({
    resolver: yupResolver(schemaValidation),
  });
  useEffect(() => {
    console.log("render");
    axios
      .get(`https://localhost:7123/api/Categories/${id}`)
      .then((response) => {
        setCategogies(response.data);
      });
  }, []);
  console.log(123);
  setValue("tenDM", categories.tenDM);
  console.log("set value");
  setValue("description", categories.description);
  const onSubmit = (data) => {
    //alert(JSON.stringify(data));
    console.log("onSubmit ~ data", data);
    if (isValid) {
      alert("Cập nhật thông tin thành công");
      console.log("onSubmit ~ data", data);
      console.log("onSubmit ~ data1", id, typeof parseInt(id));
      axios
        .put(`https://localhost:7123/api/Categories/${id}`, {
          dmspId: parseInt(id),
          tenDM: data.tenDM,
          description: data.description,
        })
        .then(navigate("/categories"))
        .catch(function (error) {
          console.log(error);
        });
    }
  };
  return (
    <form className="_form " onSubmit={handleSubmit(onSubmit)}>
      <div className="d-flex justify-content-center">
        <h3>Thông tin danh mục</h3>
      </div>
      <div className="mb-2 d-flex flex-column">
        <label htmlFor="tenDM">Tên danh mục</label>
        <input
          type="text"
          id="tenDM"
          {...register("tenDM")}
          className="form-control"
          //defaultValue={categories.tenDM}
          // {...register("tenDM")}
        />
        {errors.tenDM && (
          <div className="text-danger">{errors.tenDM.message}</div>
        )}
      </div>
      <div className="mb-2 d-flex flex-column">
        <label htmlFor="description">Mô tả</label>
        <input
          type="text"
          id="description"
          className="form-control"
          {...register("description")}
          placeholder="Vui lòng nhập tên mô tả"
        />
        {errors.description && (
          <div className="text-danger">{errors.description.message}</div>
        )}
      </div>
      <div className="d-flex ">
        <button type="submit" className="p-2 ms-auto bg-primary text-light">
          Thay đổi thông tin
        </button>
      </div>
    </form>
  );
};

export default DetailCategories;
