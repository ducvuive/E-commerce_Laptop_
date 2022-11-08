import axios from "axios";
import React, { Fragment, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useForm } from "react-hook-form";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import Button from "react-bootstrap/esm/Button";
const schemaValidation = yup.object({
  nameProduct: yup
    .string()
    .required("Vui lòng nhập danh mục")
    .max(100, "Danh mục có dưới 100 kí tự"),
  price: yup.number().required("Vui lòng nhập số tiền"),
});

const DetailProduct = () => {
  const [product, setProduct] = useState("");
  const [nameCategory, setnameCategory] = useState("");
  const [screen, setScreen] = useState([]);
  const [ram, setRam] = useState([]);
  const [processor, setProcessor] = useState([]);
  const [connect, setConnect] = useState([]);
  const [listCategory, setListCategory] = useState([]);
  console.log("DetailProduct ~ nameCategory", nameCategory);
  console.log("DetailProduct ~ product", product);
  const navigate = useNavigate();
  const { id } = useParams();
  console.log("DetailProduct ~ id", id);
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
  const loadScreen = async () => {
    await axios
      .get("https://localhost:7123/api/ManHinhs/all")
      .then((response) => {
        setScreen(response.data);
      });
  };
  const loadRam = async () => {
    await axios.get("https://localhost:7123/api/BoNhoRams").then((response) => {
      setRam(response.data);
    });
  };
  const loadProcessor = async () => {
    await axios.get("https://localhost:7123/api/BoXuLies").then((response) => {
      setProcessor(response.data);
    });
  };
  const loadConnect = async () => {
    await axios
      .get("https://localhost:7123/api/CongKetNois")
      .then((response) => {
        setConnect(response.data);
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
    loadConnect();
    loadProcessor();
    loadScreen();
    loadRam();
  }, []);
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
  setValue("number", product.soLuong);
  setValue("screen", product.manHinhId);
  setValue("processor", product.boXuLyId);
  setValue("ram", product.ramId);
  setValue("category", product.dmspId);
  setValue("connect", product.congKetNoiId);
  setValue("rating", product.danhGia);

  const onSubmit = (data) => {
    //alert(JSON.stringify(data));
    let yourDate = new Date().toISOString();
    console.log("onSubmit ~ yourDate", yourDate);
    console.log("onSubmit ~ data", data);
    if (isValid) {
      alert("Cập nhật thông tin thành công");
      axios
        .put(`https://localhost:7123/api/SanPhams/${id}`, {
          sanPhamId: id,
          manHinhId: data.screen,
          dmspId: data.category,
          congKetNoiId: data.connect,
          ramId: data.ram,
          boXuLyId: data.processor,
          tenSP: data.nameProduct,
          donGia: data.price,
          soLuong: data.number,
          ngayCapNhat: yourDate,
          ngayTao: product.ngayTao,
          danhGia: data.rating,
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
        <h3>Thông tin sản phẩm</h3>
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
            type="text"
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
            {...register("category")}
          >
            <option selected value={product.dmspId}>
              {nameCategory.tenDM}
            </option>
            {listCategory.map((item, index) => {
              if (item.tenDM != nameCategory.tenDM) {
                return (
                  <option value={item.dmspId} key={index}>
                    {item.tenDM}
                  </option>
                );
              }
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
            <option selected>{product.manHinhId}</option>
            {screen.map((item, index) => {
              return (
                <option value={item.manHinhId} key={index}>
                  {item.kichThuoc} {item.doPhanGiai}
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
            <option selected>{product.boXuLyId}</option>
            {processor.map((item, index) => {
              return (
                <option value={item.boXuLyId} key={index}>
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
            <option selected>{product.ramId}</option>
            {ram.map((item, index) => {
              return (
                <option value={item.ramId} key={item.ramId}>
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
            <option selected>{product.congKetNoiId}</option>
            {connect.map((item, index) => {
              return (
                <option value={item.congKetNoiId} key={item.congKetNoiId}>
                  {item.congKetNoiId}
                </option>
              );
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
            disabled={true}
            {...register("createDate")}
          />
        </div>
        <div className="mb-2 col d-flex flex-column">
          <label htmlFor="createDate">Ngày sửa lần cuối</label>
          <input
            type="datetime-local"
            id="createDate"
            disabled={true}
            {...register("updateDate")}
          />
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
        <div className="mb-2 col d-flex flex-column">
          <label htmlFor="rating">Đánh giá</label>
          <input
            type="number"
            className="form-control"
            id="rating"
            disabled={true}
            {...register("rating")}
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
