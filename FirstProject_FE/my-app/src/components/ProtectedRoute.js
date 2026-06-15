import React, { useEffect, useState } from "react";
import { Navigate, Outlet, useLocation } from "react-router";
import { useCookies } from "react-cookie";
import {
    ACCESS_TOKEN_COOKIE,
    REFRESH_SESSION_ID_COOKIE,
    REFRESH_TOKEN_COOKIE,
    REFRESH_USER_ID_COOKIE,
    clearAuthCookies,
    getAccessToken,
    isTokenExpired,
    refreshAccessToken,
} from "../utils/auth";
const ProtectedRoute = () => {
    const [cookies, setCookie, removeCookie] = useCookies([
        ACCESS_TOKEN_COOKIE,
        REFRESH_SESSION_ID_COOKIE,
        REFRESH_TOKEN_COOKIE,
        REFRESH_USER_ID_COOKIE,
    ]);
    const location = useLocation();
    const [authStatus, setAuthStatus] = useState("checking");

    useEffect(() => {
        let isMounted = true;

        const checkAuth = async () => {
            const token = getAccessToken();
            if (!token) {
                const refreshedToken = await refreshAccessToken();
                if (isMounted) {
                    setAuthStatus(refreshedToken ? "authenticated" : "unauthenticated");
                }
                return;
            }

            if (!isTokenExpired(token)) {
                if (isMounted) {
                    setAuthStatus("authenticated");
                }
                return;
            }

            const refreshedToken = await refreshAccessToken();
            if (!refreshedToken) {
                clearAuthCookies(removeCookie);
            }

            if (isMounted) {
                setAuthStatus(refreshedToken ? "authenticated" : "unauthenticated");
            }
        };

        checkAuth();

        return () => {
            isMounted = false;
        };
    }, [cookies, removeCookie]);

    if (authStatus === "checking") {
        return null;
    }

    return authStatus === "unauthenticated" ? (
        <Navigate to="/login" state={{ from: location }} replace />
    ) : (
        <Outlet />
    );
};

export default ProtectedRoute;
