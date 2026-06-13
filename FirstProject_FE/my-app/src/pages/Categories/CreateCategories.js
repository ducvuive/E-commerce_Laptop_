import axios from "axios";
import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
const schemaValidation = yup.object({
  nameCategory: yup
    .string()
    .required("Please enter a category")
    .max(50, "Category must be under 50 characters"),
  description: yup
    .string()
    .required("Please enter a description")
    .max(50, "Category must be under 50 characters"),
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
        <h3>Create Category</h3>
      </div>
      <div className="mb-2 d-flex flex-column">
        <label htmlFor="">Category Name</label>
        <input
          type="text"
          className="form-control"
          {...register("nameCategory")}
          placeholder="Please enter a category name"
        />
        {errors.nameCategory && (
          <div className="text-danger">{errors.nameCategory.message}</div>
        )}
      </div>
      <div className="mb-2 d-flex flex-column">
        <label htmlFor="description">Description</label>
        <input
          type="text"
          id="description"
          className="form-control"
          {...register("description")}
          defaultValue={categories.description}
          placeholder="Please enter a description"
        />
        {errors.description && (
          <div className="text-danger">{errors.description.message}</div>
        )}
      </div>
      <div className="d-flex ">
        <button className="p-2 ms-auto bg-primary text-light" type="">
          Create Category
        </button>
      </div>
    </form>
  );
};

export default CreateCategories;
