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
    .required("Please enter a product name")
        .max(150, "Product name must be under 150 characters"),
  price: yup.string().label("Price").required("Please enter the product price"),
});

const CreateProduct = () => {
  const [product, setProduct] = useState("");
  const [screen, setScreen] = useState([]);
  const [ram, setRam] = useState([]);
  const [processor, setProcessor] = useState([]);
  console.log("CreateProduct ~ processor", processor);
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
  let date = new Date();
  useEffect(() => {
    loadCate();
    loadProcessor();
    loadScreen();
    loadRam();
  }, []);
  const onSubmit = (data) => {
    console.log("onSubmit ~ data", data);
    let yourDate = new Date().toISOString();
    console.log("day", yourDate);
    if (isValid) {
      alert("Product added successfully");
      axios
        .post(`https://localhost:7123/api/Product/`, {
          screenId: data.screen,
          categoryId: data.listCategory,
          processorId: data.processor,
          ramId: data.ram,
          nameProduct: data.nameProduct,
          price: data.price,
          quantity: data.quantity,
          updatedDate: yourDate,
          publishedDate: yourDate,
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
        <h3>Create Product</h3>
      </div>
      <div className="mb-2 d-flex flex-column">
        <label htmlFor="nameProduct">Product Name</label>
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
          <label htmlFor="price">Unit Price</label>
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
          <label htmlFor="nameCategory">Product Category</label>
          <select
            className="form-select"
            aria-label="Default select example"
            {...register("listCategory")}
          >
            <option selected>Please select a category</option>
            {listCategory.map((item, index) => {
              return (
                <option value={item.categoryId} key={index}>
                  {item.name}
                </option>
              );
            })}
          </select>
        </div>
      </div>
      <div className="row">
        <div className="mb-2 col d-flex flex-column">
          <label htmlFor="nameCategory">Screen</label>
          <select
            className="form-select"
            aria-label="Default select example"
            {...register("screen")}
          >
            <option selected>Please select a screen</option>
            {screen.map((item, index) => {
              return (
                <option
                  value={item.screenId}
                  key={index}
                  //{...register("screen")}
                >
                  {item.size}
                </option>
              );
            })}
          </select>
        </div>
        <div className="mb-2 col d-flex flex-column">
          <label htmlFor="nameCategory">Processor</label>
          <select
            className="form-select"
            aria-label="Default select example"
            {...register("processor")}
          >
            <option selected>Please select a processor</option>
            {processor.map((item, index) => {
              return (
                <option value={item.processorId} key={item.processorId}>
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
            <option selected>Please select RAM</option>
            {ram.map((item, index) => {
              return (
                <option value={item.ramId} key={index}>
                  {item.typee} {item.capacity}
                </option>
              );
            })}
          </select>
        </div>
        <div className="mb-2 col d-flex flex-column">
          <label htmlFor="quantity">Quantity</label>
          <input
            type="number"
            className="form-control"
            id="quantity"
            {...register("quantity")}
          />
        </div>
      </div>
      <div className="row">
        <div className="col"></div>
      </div>
      <div className="d-flex ">
        <button type="submit" className="p-2 ms-auto bg-primary text-light">
          Create Product
        </button>
      </div>
    </form>
  );
};

export default CreateProduct;
