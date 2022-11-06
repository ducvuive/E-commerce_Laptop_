import axios from "axios";
import React, { Fragment, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useForm } from "react-hook-form";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import Button from "react-bootstrap/esm/Button";
import Moment from "react-moment";
const schemaValidation = yup.object({
  tenSP: yup
    .string()
    //.required("Vui lòng nhập danh mục")
    .max(50, "Danh mục có dưới 50 kí tự"),
  // description: yup
  //   .string()
  //   //.required("Vui lòng nhập mô tả")
  //   .max(50, "Danh mục có dưới 50 kí tự"),
});

const DetailProduct = () => {
  const [product, setProduct] = useState("");
  const [nameCategory, setnameCategory] = useState("");
  const [listCategory, setListCategory] = useState([]);
  console.log("DetailProduct ~ nameCategory", nameCategory);
  console.log("DetailProduct ~ product", product);
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
  const loadCate = async () => {
    await axios
      .get("https://localhost:7123/api/DanhMucSanPhams")
      .then((response) => {
        setListCategory(response.data);
      });
  };
  useEffect(() => {
    axios
      .get(`https://localhost:7123/api/SanPhams/admin_product/${id}`)
      .then((response) => {
        setProduct(response.data);
      });
  }, []);
  useEffect(() => {
    loadCate();
  }, []);
  // useEffect(() => {
  //   if (product) {
  //     axios
  //       .get(`https://localhost:7123/api/DanhMucSanPhams/${product.dmspId}`)
  //       .then((response) => {
  //         setnameCategory(response.data);
  //       });
  //   }
  // }, [product]);
  useEffect(() => {
    if (product) {
      axios
        .get(`https://localhost:7123/api/DanhMucSanPhams/${product.dmspId}`)
        .then((response) => {
          setnameCategory(response.data);
        });
    }
  }, [product]);

  setValue("nameProduct", product.tenSP);
  setValue("price", product.donGia);
  setValue("nameCategory", nameCategory.tenDM);
  setValue("createDate", product.ngayTao);
  setValue("updateDate", product.ngayCapNhat);
  console.log("DetailProduct ~ donGia", product.donGia);
  // setValue("description", categories.description);
  const onSubmit = (data) => {
    //alert(JSON.stringify(data));
    console.log("onSubmit ~ data", data);
    if (isValid) {
      alert("Cập nhật thông tin thành công");
    }
  };
  return (
    <form className="_form " onSubmit={handleSubmit(onSubmit)}>
      <div className="d-flex justify-content-center">
        <h3>Thông tin sản phẩm</h3>
      </div>
      <div className="mb-2 d-flex flex-column">
        <label htmlFor="nameProduct">Tên sản phẩm</label>
        {/* <textarea
          type="text"
          id="nameProduct"
          {...register("nameProduct")}
        /> */}
        <textarea
          class="form-control"
          placeholder="Leave a comment here"
          id="nameProduct"
          {...register("nameProduct")}
          style={{ height: "100px" }}
        ></textarea>
        {errors.tenDM && (
          <div className="text-danger">{errors.tenDM.message}</div>
        )}
      </div>
      <div className="row">
        <div className="mb-2 col d-flex flex-column">
          <label htmlFor="price">Đơn giá</label>
          <input
            type="text"
            class="form-control"
            id="price"
            {...register("price")}
            //defaultValue={categories.tenDM}
            // {...register("tenDM")}
          />
          {/* {errors.tenDM && (
          <div className="text-danger">{errors.tenDM.message}</div>
        )} */}
        </div>
        <div className="mb-2 col d-flex flex-column">
          <label htmlFor="nameCategory">Danh mục sản phẩm</label>
          {/* <input type="text" id="nameCategory" {...register("nameCategory")} /> */}
          <select class="form-select" aria-label="Default select example">
            <option selected>{nameCategory.tenDM}</option>
            {listCategory.map((item, index) => {
              if (item.tenDM != nameCategory.tenDM) {
                return (
                  <option value="${item.tenDM}" key={index}>
                    {item.tenDM}
                  </option>
                );
              }
              /* <option value="1">One</option>
                <option value="2">Two</option>
                <option value="3">Three</option> */
            })}
          </select>
        </div>
      </div>
      <div className="row">
        <div className="mb-2 col d-flex flex-column">
          <label htmlFor="createDate">Ngày tạo</label>
          <input
            type="datetime-local"
            id="createDate"
            disabled="true"
            {...register("createDate")}
            value={product.ngayTao}
          />
        </div>
        <div className="mb-2 col d-flex flex-column">
          <label htmlFor="createDate">Ngày sửa lần cuối</label>
          <input
            type="datetime-local"
            id="createDate"
            disabled="true"
            {...register("createDate")}
            value={product.ngayCapNhat}
          />
        </div>
      </div>
      <div className="mb-2 flex-column d-flex">
        <label>Hình ảnh</label>
        <div className="d-flex justify-content-center">
          <img
            style={{ width: "400px" }}
            src={`https://localhost:7123/wwwroot/${product.hinhAnh}`}
            alt=""
          />
        </div>
      </div>

      <div className="d-flex ">
        <button type="submit" className="p-2 ms-auto bg-primary text-light">
          Thay đổi thông tin
        </button>
      </div>
    </form>
  );
};

export default DetailProduct;
