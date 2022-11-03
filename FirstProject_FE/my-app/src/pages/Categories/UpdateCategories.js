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

const UpdateCategories = () => {
  const [categories, setCategogies] = useState();
  const navigate = useNavigate();
  console.log("Detail ~ categories", categories);
  const { id } = useParams();
  const {
    register,
    handleSubmit,
    formState: { errors, isValid },
  } = useForm({
    resolver: yupResolver(schemaValidation),
  });
  console.log("Detail ~ id", typeof id);
  useEffect(() => {
    axios
      .get(`https://localhost:7123/api/DanhMucSanPhams/${id}`)
      .then((response) => {
        setCategogies(response.data);
      });
  }, []);
  const onSubmit = (data) => {
    if (isValid) {
      //console.log("onSubmit ~ data", data);
      //console.log("onSubmit ~ data1", id, typeof parseInt(id));
      axios
        .put(`https://localhost:7123/api/DanhMucSanPhams/`, {
          dmspId: parseInt(id),
          tenDM: data.tenDM,
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
        <h3>Thông tin danh mục 1</h3>
      </div>
      <div className="mb-2 d-flex flex-column">
        <label htmlFor="">Tên danh mục</label>
        <input
          type="text"
          {...register("tenDM")}
          defaultValue={categories.tenDM}
          placeholder="Vui lòng nhập tên danh mục"
          className=""
          // {...register("tenDM")}
        />
        {errors.tenDM && (
          <div className="text-danger">{errors.tenDM.message}</div>
        )}
      </div>
      <div className="d-flex ">
        <button className="p-2 ms-auto bg-primary text-light" type="">
          Thay đổi thông tin
        </button>
        {/* <button
          type="Submit"
          className=""
        >
          {isSubmitting ? (
            <div className=""></div>
          ) : (
            "Thay đổi thông tin"
          )}
        </button> */}
      </div>
    </form>
  );
};

export default UpdateCategories;
