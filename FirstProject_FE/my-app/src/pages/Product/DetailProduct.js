import axios from "axios";
import React, { Fragment, useEffect, useState } from "react";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import { useForm } from "react-hook-form";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
const schemaValidation = yup.object({
  nameProduct: yup
    .string()
    .required("Vui lòng nhập danh mục")
    .max(200, "Danh mục có dưới 200 kí tự"),
  price: yup.number().required("Vui lòng nhập số tiền"),
});

const DetailProduct = () => {
  const location = useLocation();
  const [product, setProduct] = useState("");
  const [nameCategory, setnameCategory] = useState("");
  const [screen, setScreen] = useState([]);
  const [ram, setRam] = useState([]);
  const [processor, setProcessor] = useState([]);
  const [connect, setConnect] = useState([]);
  const [listCategory, setListCategory] = useState([]);
  const productId = location.state.productId;
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
  // const loadConnect = async () => {
  //   await axios.get("https://localhost:7123/api/Connect").then((response) => {
  //     setConnect(response.data);
  //   });
  // };
  useEffect(() => {
    axios
      .get(`https://localhost:7123/api/Product/admin_product/${productId}`)
      .then((response) => {
        setProduct(response.data);
      });
  }, []);
  useEffect(() => {
    loadCate();
    // loadConnect();
    loadProcessor();
    loadScreen();
    loadRam();
  }, []);
  useEffect(() => {
    if (product) {
      axios
        .get(`https://localhost:7123/api/Categories/${product.categoryId}`)
        .then((response) => {
          setnameCategory(response.data);
        });
    }
  }, [product]);

  setValue("nameProduct", product.nameProduct);
  setValue("price", product.price);
  setValue("nameCategory", nameCategory);
  setValue("createDate", product.publishedDate);
  setValue("updateDate", product.updatedDate);
  setValue("number", product.quantity);
  setValue("screen", product.screenId);
  setValue("processor", product.processorId);
  setValue("ram", product.ramId);
  setValue("category", product.categoryId);
  setValue("rating", product.rating);

  const onSubmit = (data) => {
    let yourDate = new Date().toISOString();
    if (isValid) {
      alert("Cập nhật thông tin thành công");
      axios
        .put(`https://localhost:7123/api/Product/${productId}`, {
          productId: productId,
          screenId: data.screen,
          categoryId: data.category,
          processorId: data.processor,
          ramId: data.ram,
          nameProduct: data.nameProduct,
          price: data.price,
          quantity: data.number,
          updatedDate: yourDate,
          publishedDate: product.ngayTao,
          rating: data.rating,
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
            <option selected value={product.categoryId}>
              {nameCategory.name}
            </option>
            {listCategory.map((item, index) => {
              if (item.name != nameCategory) {
                return (
                  <option value={item.categoryId} key={index}>
                    {item.name}
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
            <option selected>{product.screenId}</option>
            {screen.map((item, index) => {
              return (
                <option value={item.screenId} key={index}>
                  {item.size}
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
                <option value={item.processorId} key={index}>
                  {item.cpuTechnology}
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
                  {item.typee} {item.capacity}
                </option>
              );
            })}
          </select>
        </div>
        <div className="mb-2 col d-flex flex-column">
          <label htmlFor="number">Số lượng</label>
          <input
            type="number"
            className="form-control"
            id="number"
            {...register("number")}
          />
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
            src={`https://localhost:7123/wwwroot/${product.image}`}
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
