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
            Welcome to the admin page
          </h2>
          <p className="fs-4 text-light text-uppercase">
            Have a great day
          </p>
        </div>
      ) : userRole === null ? null : (
        <div className="text-center z-index">
          <h2 className="fs-1 text-light text-uppercase">
            You do not have permission to access this page
          </h2>
          <p className="fs-4 text-light text-uppercase">
            Have a great day
          </p>
        </div>
      )}
    </div>
  );
};

export default Dashboard;
