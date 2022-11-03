import React from "react";
import { Link } from "react-router-dom";

const NavBar = () => {
  return (
    <div className="">
      <nav className="navbar navbar-expand-lg navbar-light bg-light">
        <div className="px-5 container-fluid">
          <a className="navbar-brand fw-bold fs-4 text-uppercase" href="/home">
            laptopstore
          </a>
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
          <div className="collapse navbar-collapse" id="navbarSupportedContent">
            <ul className="mb-2 navbar-nav me-auto mb-lg-0"></ul>
            <ul className="d-flex navbar-nav">
              <li className="nav-item">
                <Link className="nav-link text-dark">Đăng kí</Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link text-dark">Đăng nhập</Link>
              </li>

              <li className="nav-item">
                <a className="nav-link text-dark" href="/cart">
                  <i className="fa-solid fa-cart-shopping"></i>
                </a>
              </li>
            </ul>
          </div>
        </div>
      </nav>
    </div>
  );
};

export default NavBar;
