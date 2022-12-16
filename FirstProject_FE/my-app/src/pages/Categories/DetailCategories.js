import axios from "axios";
import React, { Fragment, useEffect, useState } from "react";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import { useForm } from "react-hook-form";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
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
  const location = useLocation();
  const [categories, setCategogies] = useState("");
  const navigate = useNavigate();
  const idCategory = location.state.idCategory;
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
      .get(`https://localhost:7123/api/Categories/${idCategory}`)
      .then((response) => {
        setCategogies(response.data);
      });
  }, []);

  setValue("tenDM", categories.name);
  setValue("description", categories.description);
  const onSubmit = (data) => {
    console.log("onSubmit ~ data", data);
    console.log("onSubmit ~ idCategory", idCategory);
    if (isValid) {
      alert("Cập nhật thông tin thành công");
      axios
        .put(`https://localhost:7123/api/Categories/${idCategory}`, {
          categoryId: parseInt(idCategory),
          name: data.tenDM,
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
