import axios from "axios";
import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
const schemaValidation = yup.object({
  nameCategory: yup
    .string()
    .required("Vui lòng nhập danh mục")
    .max(50, "Danh mục có dưới 50 kí tự"),
  description: yup
    .string()
    .required("Vui lòng nhập mô tả")
    .max(50, "Danh mục có dưới 50 kí tự"),
});
const CreateCategories = () => {
  const [categories, setCategogies] = useState("");
  const navigate = useNavigate();
  console.log("Detail ~ categories", categories);
  const {
    register,
    handleSubmit,
    formState: { errors, isValid },
  } = useForm({
    resolver: yupResolver(schemaValidation),
  });
  const value = {
    nameCategory: categories,
  };
  const onSubmit = (data) => {
    if (isValid) {
      console.log(data.nameCategory);
      axios
        .post(`https://localhost:7123/api/Categories/`, {
          name: data.nameCategory,
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
        <h3>Tạo danh mục</h3>
      </div>
      <div className="mb-2 d-flex flex-column">
        <label htmlFor="">Tên danh mục</label>
        <input
          type="text"
          className="form-control"
          {...register("nameCategory")}
          placeholder="Vui lòng nhập tên danh mục"
        />
        {errors.nameCategory && (
          <div className="text-danger">{errors.nameCategory.message}</div>
        )}
      </div>
      <div className="mb-2 d-flex flex-column">
        <label htmlFor="description">Mô tả</label>
        <input
          type="text"
          id="description"
          className="form-control"
          {...register("description")}
          defaultValue={categories.description}
          placeholder="Vui lòng nhập tên mô tả"
        />
        {errors.description && (
          <div className="text-danger">{errors.description.message}</div>
        )}
      </div>
      <div className="d-flex ">
        <button className="p-2 ms-auto bg-primary text-light" type="">
          Tạo danh mục
        </button>
      </div>
    </form>
  );
};

export default CreateCategories;
