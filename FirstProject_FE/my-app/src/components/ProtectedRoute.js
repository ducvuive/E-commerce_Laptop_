import React, { useContext } from "react";
import { Navigate, Outlet, useLocation } from "react-router";
// import useAuth from "../hooks/useAuth";
import { useCookies } from "react-cookie";
import jwt_decode from "jwt-decode";
const ProtectedRoute = () => {
    // const { auth } = useAuth();
    const [cookies, setCookie, removeCookie] = useCookies(["token"]);
    const location = useLocation();
    const token = cookies["token"];
    var role = "";
    if (token) {
        const decoded = jwt_decode(token);
        role = decoded.role;
    }

    return role === "" ? (
        <Navigate to="/login" state={{ from: location }} replace />
    ) : (
        <Outlet />
    );
};

export default ProtectedRoute;