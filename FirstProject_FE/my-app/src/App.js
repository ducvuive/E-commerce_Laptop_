import logo from "./logo.svg";
import "bootstrap/dist/css/bootstrap.min.css";
import { Fragment } from "react";
import Container from "react-bootstrap/Container";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import "./App.css";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Dashboard from "./pages/Dashboard";
import ListProduct from "./pages/ListProduct";
import Categories from "./pages/Categories";
import SlideBar from "./components/SlideBar";
import NavBar from "./components/NavBar";
import DetailCategories from "./pages/Categories/DetailCategories";
import CreateCategories from "./pages/Categories/CreateCategories";
import Users from "./pages/Users";
import DetailUser from "./pages/Users/DetailUser";
import DetailProduct from "./pages/Product/DetailProduct";
import CreateProduct from "./pages/Product/CreateProduct";
import Login from "./components/login/Login";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import ProtectedRoute from "./components/ProtectedRoute";
import ProtectedRouteAdmin from "./components/ProtectedRouteAdmin";
import { getAccessToken, refreshAccessToken } from "./utils/auth";
axios.interceptors.request.use((config) => {
  const token = getAccessToken();
  if (token && !config.headers?.Authorization) {
    config.headers = {
      ...config.headers,
      Authorization: `Bearer ${token}`,
    };
  }

  return config;
});
axios.interceptors.response.use(
  (response) => {
    return response;
  },
  async (error) => {
    console.log("error.response.status", error);
    const originalRequest = error.config;

    if (error.response?.status === 401 && originalRequest && !originalRequest._retry) {
      originalRequest._retry = true;
      const token = await refreshAccessToken();
      if (token) {
        originalRequest.headers = {
          ...originalRequest.headers,
          Authorization: `Bearer ${token}`,
        };
        return axios(originalRequest);
      }
    }

    return Promise.reject(error);
  }
);
function App() {
  return (
    <BrowserRouter>
      <NavBar></NavBar>
      <SlideBar>
        <Routes>
          <Route path="/login" element={<Login />}></Route>
          <Route element={<ProtectedRoute />}>
            <Route path="/" element={<Dashboard />}></Route>
            <Route element={<ProtectedRouteAdmin />}>
              <Route path="/listProduct" element={<ListProduct />}></Route>
              <Route path="/users" element={<Users />}></Route>
              <Route path="/users/detail-user" element={<DetailUser />}></Route>
              <Route path="/categories" element={<Categories />}></Route>
              <Route
                path="/categories/detail-categogy"
                element={<DetailCategories />}
              ></Route>
              <Route
                path="/listProduct/detail-product"
                element={<DetailProduct />}
              ></Route>
              <Route
                path="/listProduct/Create"
                element={<CreateProduct />}
              ></Route>
              <Route
                path="/categories/Create"
                element={<CreateCategories />}
              ></Route>
            </Route>
          </Route>
        </Routes>
      </SlideBar>
    </BrowserRouter>
  );
}

export default App;
