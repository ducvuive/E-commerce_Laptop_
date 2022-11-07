import React, { useState } from "react";
import {
  FaTh,
  FaBars,
  FaClipboardList,
  FaRegChartBar,
  FaCommentAlt,
  FaShoppingBag,
  FaUserEdit,
  FaAlignJustify,
} from "react-icons/fa";
import { NavLink } from "react-router-dom";

const Sidebar = ({ children }) => {
  const menuItem = [
    {
      path: "/dashboard",
      name: "Tổng quan",
      icon: <FaTh />,
    },
    {
      path: "/listProduct",
      name: "Sản phẩm",
      icon: <FaClipboardList />,
    },
    {
      path: "/categories",
      name: "Danh mục",
      icon: <FaAlignJustify />,
    },
    {
      path: "/users",
      name: "Người dùng",
      icon: <FaUserEdit />,
    },
  ];
  return (
    <div className="container-fluid">
      <div style={{ width: "200px" }} className="sidebar bg-secondary">
        <div className="top_section"></div>
        {menuItem.map((item, index) => (
          <NavLink
            to={item.path}
            key={index}
            className="link"
            // activeClassName="active"
          >
            <div className="icon">{item.icon}</div>
            <div style={{ display: "block" }} className="link_text">
              {item.name}
            </div>
          </NavLink>
        ))}
      </div>
      <main>{children}</main>
    </div>
  );
};

export default Sidebar;
