import axios from "axios";
import { Cookies } from "react-cookie";
import jwt_decode from "jwt-decode";

export const ACCESS_TOKEN_COOKIE = "token";
export const REFRESH_TOKEN_COOKIE = "refreshToken";
export const REFRESH_USER_ID_COOKIE = "refreshUserId";
export const AUTH_CHANGED_EVENT = "auth-changed";

const API_BASE_URL = "https://localhost:7123";
const cookieStore = new Cookies();
let refreshPromise = null;

const cookieOptions = (expiresAtUtc) => {
  const options = {
    path: "/",
    sameSite: "strict",
  };

  if (window.location.protocol === "https:") {
    options.secure = true;
  }

  if (expiresAtUtc) {
    options.expires = new Date(expiresAtUtc);
  }

  return options;
};

const emitAuthChanged = () => {
  window.dispatchEvent(new Event(AUTH_CHANGED_EVENT));
};

export const getAccessToken = () => cookieStore.get(ACCESS_TOKEN_COOKIE);

export const getDecodedToken = (token = getAccessToken()) => {
  if (!token) {
    return null;
  }

  try {
    return jwt_decode(token);
  } catch {
    return null;
  }
};

export const isTokenExpired = (token = getAccessToken()) => {
  const decoded = getDecodedToken(token);
  if (!decoded?.exp) {
    return true;
  }

  return decoded.exp * 1000 <= Date.now();
};

export const getUserEmail = (token = getAccessToken()) => {
  const decoded = getDecodedToken(token);
  return decoded?.email || decoded?.sub || "";
};

export const getUserRole = (token = getAccessToken()) => {
  const decoded = getDecodedToken(token);
  const role =
    decoded?.role ||
    decoded?.["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

  return Array.isArray(role) ? role[0] : role || "";
};

export const persistAuthResponse = (data, setCookie) => {
  const accessToken = data?.accessToken || data?.value || data;
  if (!accessToken) {
    return;
  }

  const writeCookie = setCookie || cookieStore.set.bind(cookieStore);
  writeCookie(ACCESS_TOKEN_COOKIE, accessToken, cookieOptions(data?.expiresAtUtc));

  if (data?.refreshToken) {
    writeCookie(
      REFRESH_TOKEN_COOKIE,
      data.refreshToken,
      cookieOptions(data?.refreshTokenExpiresAtUtc)
    );
  }

  if (data?.userId) {
    writeCookie(
      REFRESH_USER_ID_COOKIE,
      data.userId,
      cookieOptions(data?.refreshTokenExpiresAtUtc)
    );
  }

  emitAuthChanged();
};

export const clearAuthCookies = (removeCookie) => {
  const remove = removeCookie || cookieStore.remove.bind(cookieStore);
  remove(ACCESS_TOKEN_COOKIE, { path: "/" });
  remove(REFRESH_TOKEN_COOKIE, { path: "/" });
  remove(REFRESH_USER_ID_COOKIE, { path: "/" });
  emitAuthChanged();
};

const requestNewAccessToken = async () => {
  const refreshToken = cookieStore.get(REFRESH_TOKEN_COOKIE);
  const userId = cookieStore.get(REFRESH_USER_ID_COOKIE);

  if (!refreshToken || !userId) {
    return null;
  }

  try {
    const response = await axios.post(`${API_BASE_URL}/Auth/refresh`, {
      userId,
      refreshToken,
    });

    persistAuthResponse(response.data);
    return response.data?.accessToken || response.data?.value || null;
  } catch {
    clearAuthCookies();
    return null;
  }
};

export const refreshAccessToken = async () => {
  if (!refreshPromise) {
    refreshPromise = requestNewAccessToken().finally(() => {
      refreshPromise = null;
    });
  }

  return refreshPromise;
};

export const authHeader = () => ({
  headers: {
    Authorization: `Bearer ${getAccessToken() || ""}`,
  },
});
