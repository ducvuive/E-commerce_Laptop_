import axios from "axios";
import React, { Fragment, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useForm } from "react-hook-form";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import Button from "react-bootstrap/esm/Button";
import Moment from "react-moment";
const schemaValidation = yup.object({
  nameProduct: yup
    .string()
    .required("Vui lòng nhập tên sản phẩm")
    .max(50, "Tên sản phẩm có dưới 50 kí tự"),
  price: yup.string().label("Giá").required("Vui lòng nhập giá sản phấm"),
});

const CreateProduct = () => {
  const [product, setProduct] = useState("");
  const [screen, setScreen] = useState([]);
  console.log("CreateProduct ~ screen", screen);
  const [ram, setRam] = useState([]);
  const [processor, setProcessor] = useState([]);
  const [connect, setConnect] = useState([]);
  const [listCategory, setListCategory] = useState([]);

  const navigate = useNavigate();
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
      .get("https://localhost:7123/api/Categories/GetCate")
      .then((response) => {
        setListCategory(response.data);
      });
  };
  const loadScreen = async () => {
    await axios
      .get("https://localhost:7123/api/Screen/all")
      .then((response) => {
        setScreen(response.data);
      });
  };
  const loadRam = async () => {
    await axios.get("https://localhost:7123/api/Ram").then((response) => {
      setRam(response.data);
    });
  };
  const loadProcessor = async () => {
    await axios.get("https://localhost:7123/api/Processor").then((response) => {
      setProcessor(response.data);
    });
  };
  const loadConnect = async () => {
    await axios.get("https://localhost:7123/api/Connect").then((response) => {
      setConnect(response.data);
    });
  };
  let date = new Date();
  useEffect(() => {
    loadCate();
    loadConnect();
    loadProcessor();
    loadScreen();
    loadRam();
  }, []);
  const onSubmit = (data) => {
    console.log("onSubmit ~ data", data);
    let yourDate = new Date().toISOString();
    //const offset = yourDate.getTimezoneOffset();
    console.log("day", yourDate);
    if (isValid) {
      alert("Thêm sản phẩm thành công");
      axios
        .post(`https://localhost:7123/api/Product/`, {
          manHinhId: data.screen,
          dmspId: data.listCategory,
          congKetNoiId: data.connect,
          ramId: data.ram,
          boXuLyId: data.processor,
          tenSP: data.nameProduct,
          donGia: data.price,
          soLuong: data.number,
          ngayCapNhat: yourDate,
          ngayTao: yourDate,
        })
        .then(navigate("/listProduct"))
        .catch(function (error) {
          console.log(error);
        });
    }
  };
  return (
    <form className="_form " onSubmit={handleSubmit(onSubmit)}>
      <div className="d-flex justify-content-center">
        <h3>Tạo sản phẩm</h3>
      </div>
      <div className="mb-2 d-flex flex-column">
        <label htmlFor="nameProduct">Tên sản phẩm</label>
        <textarea
          className="form-control"
          placeholder="Leave a comment here"
          id="nameProduct"
          {...register("nameProduct")}
          style={{ height: "100px" }}
        ></textarea>
        {errors.nameProduct && (
          <div className="text-danger">{errors.nameProduct.message}</div>
        )}
      </div>
      <div className="row">
        <div className="mb-2 col d-flex flex-column">
          <label htmlFor="price">Đơn giá</label>
          <input
            type="number"
            className="form-control"
            id="price"
            {...register("price")}
          />
          {errors.price && (
            <div className="text-danger">{errors.price.message}</div>
          )}
        </div>
        <div className="mb-2 col d-flex flex-column">
          <label htmlFor="nameCategory">Danh mục sản phẩm</label>
          <select
            className="form-select"
            aria-label="Default select example"
            {...register("listCategory")}
          >
            <option selected>Vui lòng chọn danh mục</option>
            {listCategory.map((item, index) => {
              return (
                <option value={item.dmspId} key={index}>
                  {item.tenDM}
                </option>
              );
            })}
          </select>
        </div>
      </div>
      <div className="row">
        <div className="mb-2 col d-flex flex-column">
          <label htmlFor="nameCategory">Màn hình</label>
          <select
            className="form-select"
            aria-label="Default select example"
            {...register("screen")}
          >
            <option selected>Vui lòng chọn màn hình</option>
            {screen.map((item, index) => {
              return (
                <option
                  value={item.manHinhId}
                  key={index}
                  //{...register("screen")}
                >
                  {index} {item.manHinhId} {item.kichThuoc} {item.doPhanGiai}
                </option>
              );
            })}
          </select>
        </div>
        <div className="mb-2 col d-flex flex-column">
          <label htmlFor="nameCategory">Bộ xử lý</label>
          <select
            className="form-select"
            aria-label="Default select example"
            {...register("processor")}
          >
            <option selected>Vui lòng chọn bộ xử lý</option>
            {processor.map((item, index) => {
              return (
                <option value={item.boXuLyId} key={item.boXuLyId}>
                  {item.congNgheCPU}
                </option>
              );
            })}
          </select>
        </div>
      </div>
      <div className="row">
        <div className="mb-2 col d-flex flex-column">
          <label htmlFor="nameCategory">Ram</label>
          <select
            className="form-select"
            aria-label="Default select example"
            {...register("ram")}
          >
            <option selected>Vui lòng chọn ram</option>
            {ram.map((item, index) => {
              return (
                <option value={item.ramId} key={index}>
                  {item.loaiRam} {item.dungLuongRam}
                </option>
              );
            })}
          </select>
        </div>
        <div className="mb-2 col d-flex flex-column">
          <label htmlFor="nameCategory">Cổng kết nối</label>
          <select
            className="form-select"
            aria-label="Default select example"
            {...register("connect")}
          >
            <option selected>Vui lòng chọn cổng kết nối</option>
            {connect.map((item, index) => {
              return (
                <option value={item.congKetNoiId} key={index}>
                  {item.congKetNoiId}
                </option>
              );
            })}
          </select>
        </div>
      </div>
      <div className="row">
        <div className="mb-2 col d-flex flex-column">
          <label htmlFor="number">Số lượng</label>
          <input
            type="number"
            className="form-control"
            id="number"
            {...register("number")}
          />
        </div>
        <div className="col"></div>
      </div>
      <div className="d-flex ">
        <button type="submit" className="p-2 ms-auto bg-primary text-light">
          Tạo sản phẩm
        </button>
      </div>
    </form>
  );
};

export default CreateProduct;
