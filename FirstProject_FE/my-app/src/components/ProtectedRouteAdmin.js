import React, { useEffect, useState } from "react";
import { Navigate, Outlet, useLocation } from "react-router";
import { useCookies } from "react-cookie";
import {
    ACCESS_TOKEN_COOKIE,
    REFRESH_SESSION_ID_COOKIE,
    REFRESH_TOKEN_COOKIE,
    REFRESH_USER_ID_COOKIE,
    getAccessToken,
    getUserRole,
    isTokenExpired,
    refreshAccessToken,
} from "../utils/auth";
const ProtectedRouteAdmin = () => {
    const [cookies] = useCookies([
        ACCESS_TOKEN_COOKIE,
        REFRESH_SESSION_ID_COOKIE,
        REFRESH_TOKEN_COOKIE,
        REFRESH_USER_ID_COOKIE,
    ]);
    const location = useLocation();
    const [isAdmin, setIsAdmin] = useState(null);

    useEffect(() => {
        let isMounted = true;

        const checkAdmin = async () => {
            let token = getAccessToken();
            if (!token || isTokenExpired(token)) {
                token = await refreshAccessToken();
            }

            if (isMounted) {
                setIsAdmin(getUserRole(token) === "Admin");
            }
        };

        checkAdmin();

        return () => {
            isMounted = false;
        };
    }, [cookies]);

    if (isAdmin === null) {
        return null;
    }

    return isAdmin ? (
        <Outlet />
    ) : (<Navigate to="/" state={{ from: location }} replace />)
};

export default ProtectedRouteAdmin;
