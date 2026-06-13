import { useState, useEffect, createContext } from "react";
import { useCookies } from "react-cookie";
import {
    ACCESS_TOKEN_COOKIE,
    AUTH_CHANGED_EVENT,
    getAccessToken,
    getUserRole,
    isTokenExpired,
    refreshAccessToken,
} from "../utils/auth";
export const UserRoleContext = createContext(null);

const UserRoleProvider = ({ children }) => {
    const [cookies, setCookie, removeCookie] = useCookies([ACCESS_TOKEN_COOKIE]);
    const [userRole, setUserRole] = useState(null);
    const loadCate = async () => {
        let token = getAccessToken();
        if (!token || isTokenExpired(token)) {
          token = await refreshAccessToken();
        }

        setUserRole(token ? getUserRole(token) : "");
      };
      useEffect(() => {
        loadCate();
        window.addEventListener(AUTH_CHANGED_EVENT, loadCate);
        return () => window.removeEventListener(AUTH_CHANGED_EVENT, loadCate);
      }, [cookies]);
    // useEffect(() => {
    //     axios
    //         .get("/api/users")
    //         .then((response) =>
    //             setUserRole(response.data.type === 1 ? "admin" : "staff")
    //         );
    // }, []);

    return (
        <UserRoleContext.Provider value={userRole}>
            {children}
        </UserRoleContext.Provider>
    );
};
export default UserRoleProvider;
