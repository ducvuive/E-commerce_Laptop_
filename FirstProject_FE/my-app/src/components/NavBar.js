import React, { useEffect, useState } from "react";
import { useCookies } from "react-cookie";
import { Link } from "react-router-dom";
import {
  ACCESS_TOKEN_COOKIE,
  AUTH_CHANGED_EVENT,
  REFRESH_SESSION_ID_COOKIE,
  REFRESH_TOKEN_COOKIE,
  REFRESH_USER_ID_COOKIE,
  clearAuthCookies,
  getAccessToken,
  getUserEmail,
  isTokenExpired,
  revokeAuthSession,
} from "../utils/auth";
const NavBar = () => {
  const [cookies, setCookie, removeCookie] = useCookies([
    ACCESS_TOKEN_COOKIE,
    REFRESH_SESSION_ID_COOKIE,
    REFRESH_TOKEN_COOKIE,
    REFRESH_USER_ID_COOKIE,
  ]);
  const [user, setUser] = useState("");
  const [token, setToken] = useState("");
  const loadCate = async () => {
    const token = getAccessToken();
    if (token && !isTokenExpired(token)) {
      setUser(getUserEmail(token));
      setToken(token);
      return;
    }

    setUser("");
    setToken("");
  };
  useEffect(() => {
    loadCate();
    window.addEventListener(AUTH_CHANGED_EVENT, loadCate);
    return () => window.removeEventListener(AUTH_CHANGED_EVENT, loadCate);
  }, [cookies]);

  async function Logout() {
    if (token == "") {
      console.log("nullnull");
    }
    await revokeAuthSession();
    clearAuthCookies(removeCookie);
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
                  {/* <Link className="nav-link text-dark">Register</Link> */}
                </li>
                <li className="nav-item">
                  <Link to={"/login"} className="nav-link text-dark">
                    Login
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
                    Logout
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
