import { useState, useEffect, createContext } from "react";
import axios from "axios";
import { useCookies } from "react-cookie";
import jwt_decode from "jwt-decode";
export const UserRoleContext = createContext();

const UserRoleProvider = ({ children }) => {
    const [cookies, setCookie, removeCookie] = useCookies(["token"]);
    const [userRole, setUserRole] = useState("");
    const loadCate = async () => {
        const token = cookies["token"];
        if (cookies != null) {
          const decoded = jwt_decode(token);
          //console.log("loadCate ~ decoded", decoded);
          setUserRole(decoded.role)
        }
        //console.log("loadCate ~ decoded", decoded);
      };
      useEffect(() => {
        loadCate();
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
