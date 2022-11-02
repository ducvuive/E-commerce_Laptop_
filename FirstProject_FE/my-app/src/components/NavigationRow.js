import React from "react";
import style from "./NavigationRow.module.css";
import TableViewIcon from "@mui/icons-material/TableView";
import {
  BrowserRouter as Router,
  Route,
  Link,
  NavLink,
  Routes,
} from "react-router-dom";
const NavigationRow = () => {
  return (
    <div className={`${style.full_screen} bg-secondary ps-4`}>
      {/* <Routes>
        <Route path="/" element={Catelogy} />
      </Routes> */}
      <div>
        {/* <h3 className="fw-bold fs-5">Sản phẩm</h3> */}
        <div className="mt-4"></div>
        {/* <li className="mt-4">
            <TableViewIcon />
            <Route path="/" exact component={CPU} />
          </li>
          <li className="mt-4">
            <TableViewIcon />
            <Route path="/" exact component={Ram} />
          </li>
          <li className="mt-4">
            <TableViewIcon />
            <Route path="/" exact component={Screen} />
          </li>
          <li className="mt-4">
            <TableViewIcon />
            <Route path="/" exact component={ListProduct} />
          </li> */}
      </div>
      {/* <div>
        <h3 className="fw-bold fs-5">Người dùng</h3>
        <ul>
          <li className="mt-4">
            <TableViewIcon />
            <a href="#">Danh mục</a>
          </li>
          <li className="mt-4">
            <TableViewIcon />
            <a href="#">CPU</a>
          </li>
          <li className="mt-4">
            <TableViewIcon />
            <a href="#">RAM</a>
          </li>
          <li className="mt-4">
            <TableViewIcon />
            <a href="#">Màn hình</a>
          </li>
          <li className="mt-4">
            <TableViewIcon />
            <a href="#">Danh sách các sản phẩm</a>
          </li>
        </ul>
      </div> */}
    </div>
  );
};

export default NavigationRow;
