import axios from "axios";
import React, { Fragment, useEffect, useState } from "react";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import { useForm } from "react-hook-form";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
const schemaValidation = yup.object({
  categoryName: yup
    .string()
    .required("Please enter a category")
    .max(50, "Category must be under 50 characters"),
  description: yup
    .string()
    .required("Please enter a description")
    .max(50, "Category must be under 50 characters"),
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

  setValue("categoryName", categories.name);
  setValue("description", categories.description);
  const onSubmit = (data) => {
    console.log("onSubmit ~ data", data);
    console.log("onSubmit ~ idCategory", idCategory);
    if (isValid) {
      alert("Information updated successfully");
      axios
        .put(`https://localhost:7123/api/Categories/${idCategory}`, {
          categoryId: parseInt(idCategory),
          name: data.categoryName,
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
        <h3>Category Details</h3>
      </div>
      <div className="mb-2 d-flex flex-column">
        <label htmlFor="categoryName">Category Name</label>
        <input
          type="text"
          id="categoryName"
          {...register("categoryName")}
          className="form-control"
        />
        {errors.categoryName && (
          <div className="text-danger">{errors.categoryName.message}</div>
        )}
      </div>
      <div className="mb-2 d-flex flex-column">
        <label htmlFor="description">Description</label>
        <input
          type="text"
          id="description"
          className="form-control"
          {...register("description")}
          placeholder="Please enter a description"
        />
        {errors.description && (
          <div className="text-danger">{errors.description.message}</div>
        )}
      </div>
      <div className="d-flex ">
        <button type="submit" className="p-2 ms-auto bg-primary text-light">
          Update Information
        </button>
      </div>
    </form>
  );
};

export default DetailCategories;
