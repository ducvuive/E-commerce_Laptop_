import React, { useContext } from "react";
import { UserRoleContext } from "../context/UserRoleContext";

const Dashboard = () => {
  const userRole = useContext(UserRoleContext);
  return (
    <div className="admin d-flex align-items-center justify-content-center">
    <div className="admin_layer"></div>
      {userRole === "Admin" ? (
        <div className="text-center z-index">
          <h2 className="fs-1 text-light text-uppercase">
            Xin chào bạn đến với trang admin
          </h2>
          <p className="fs-4 text-light text-uppercase">
            Chúc bạn 1 ngày tốt lành
          </p>
        </div>
      ) : (
        <div className="text-center z-index">
          <h2 className="fs-1 text-light text-uppercase">
            Bạn không có quyền truy cập trang này
          </h2>
          <p className="fs-4 text-light text-uppercase">
            Chúc bạn 1 ngày tốt lành
          </p>
        </div>
      )}
    </div>
  );
};

export default Dashboard;
