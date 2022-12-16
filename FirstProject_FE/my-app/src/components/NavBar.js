import React, { useEffect, useState } from "react";
import { useCookies } from "react-cookie";
import { Link } from "react-router-dom";
import jwt_decode from "jwt-decode";
const NavBar = () => {
  const [cookies, setCookie, removeCookie] = useCookies(["token"]);
  const [user, setUser] = useState("");
  const [token, setToken] = useState("");
  const loadCate = async () => {
    //const cookies = new Cookies();
    const token = cookies["token"];
    if (cookies != null) {
      const decoded = jwt_decode(token);
      setUser(decoded.email);
      setToken(token);
    }
  };
  useEffect(() => {
    loadCate();
  }, [cookies]);

  function Logout() {
    if (token == "") {
      console.log("nullnull");
    }
    removeCookie("token", { path: "/" });
    setToken("");
  }
  //console.log("cookie123", cookies.get("token"));
  //const token = cookies.get("token");
  return (
    <div className="">
      <nav className="navbar navbar-expand-lg navbar-light bg-light">
        <div className="px-5 container-fluid">
          <Link className="navbar-brand fw-bold fs-4 text-uppercase" to={"/"}>
            laptopstore
          </Link>
          <button
            className="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarSupportedContent"
            aria-controls="navbarSupportedContent"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>
          {token === "" ? (
            <div
              className="collapse navbar-collapse"
              id="navbarSupportedContent"
            >
              <ul className="mb-2 navbar-nav me-auto mb-lg-0"></ul>
              <ul className="d-flex navbar-nav">
                <li className="nav-item">
                  {/* <Link className="nav-link text-dark">Đăng kí</Link> */}
                </li>
                <li className="nav-item">
                  <Link to={"/login"} className="nav-link text-dark">
                    Đăng nhập
                  </Link>
                </li>
              </ul>
            </div>
          ) : (
            <div
              className="collapse navbar-collapse"
              id="navbarSupportedContent"
            >
              <ul className="mb-2 navbar-nav me-auto mb-lg-0"></ul>
              <ul className="d-flex navbar-nav">
                <li className="nav-item">
                  <p className="nav-link text-dark">{user}</p>
                </li>
                <li className="nav-item">
                  <p className="nav-link text-dark" onClick={() => Logout()}>
                    Đăng xuất
                  </p>
                </li>
              </ul>
            </div>
          )}
        </div>
      </nav>
    </div>
  );
};

export default NavBar;
