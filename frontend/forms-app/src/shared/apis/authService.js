import urls from "../constants/urls.ts";
import axios from "axios";
import Cookies from "js-cookie";
import { jwtDecode } from "jwt-decode";
import { toaster } from "evergreen-ui";
export const Register = async (request) => {
    return await axios.post(`${urls.AUTH.REGISTER}`, request)
        .then(response => {
        if (response.data.result) {
            return response;
        }
    })
        .catch(exception => {
        setTimeout(() => {
            toaster.danger('Аккаунт', {
                description: 'При авторизації виникла помилка. Перевірте вхідні дані.',
                duration: 5
            });
        }, 0);
        console.error(exception);
    });
};
export const Authenticate = async (request) => {
    return await axios.post(`${urls.AUTH.AUTHENTICATE}`, request)
        .then(response => {
        if (response.data.result) {
            Cookies.set("accessToken", response.data.result.accessToken);
            Cookies.set("refreshToken", response.data.result.refreshToken);
        }
        return response;
    })
        .catch(exception => {
        setTimeout(() => {
            toaster.danger('Аккаунт', {
                description: 'При авторизації виникла помилка. Перевірте вхідні дані.',
                duration: 5
            });
        }, 0);
        console.error(exception);
        return null;
    });
};
export const Deauthenticate = async () => {
    const accessCookieData = Cookies.get("accessToken");
    const refreshCookieData = Cookies.get("refreshToken");
    if (accessCookieData && refreshCookieData) {
        Cookies.remove("accessToken");
        Cookies.remove("refreshToken");
    }
    return true;
};
export const GetAccessTokenFromCookies = () => {
    const accessToken = Cookies.get("accessToken");
    return accessToken || null;
};
export const GetUserFromToken = () => {
    const token = GetAccessTokenFromCookies();
    if (!token) {
        return null;
    }
    try {
        return jwtDecode(token);
    }
    catch (error) {
        console.error("Invalid token", error);
        return null;
    }
};
export const isAuthenticated = () => {
    const accessToken = Cookies.get("accessToken");
    return !!accessToken;
};
