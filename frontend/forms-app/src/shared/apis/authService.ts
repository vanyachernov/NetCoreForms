import urls from "../constants/urls.ts";
import axios, {request} from "axios";
import Cookies from "js-cookie";
import {jwtDecode} from "jwt-decode";

export interface AuthenticateUserRequest {
    email: string;
    password: string;
}

export interface RegisterUserRequest {
    fullName: {
        lastName: string;
        firstName: string;
    },
    email: {
        email: string;
    },
    password: {
        password: string;
    }
}

interface UserPayload {
    userId: string;
    userEmail: string;
    userFirstname: string;
    userLastname: string;
    userRole: string;
    exp: number;
}

export const Register = async (request: RegisterUserRequest) => {
    return await axios.post(
        `${urls.AUTH.REGISTER}`,
        request)
        .then(response => {
            if (response.data.result) {
                return response;
            }
        })
        .catch(exception => {
            console.error(exception);
            throw exception;
        });
}

export const Authenticate = async (request: AuthenticateUserRequest) => {
    return await axios.post(
        `${urls.AUTH.AUTHENTICATE}`,
        request)
        .then(response => {
            if (response.data.result) {
                Cookies.set("accessToken", response.data.result.accessToken);
                Cookies.set("refreshToken", response.data.result.refreshToken);
            }
            
            return response;
        })
        .catch(exception => {
            console.error(exception);
            throw exception;
        });
}

export const Deauthenticate = async () => {
    const accessCookieData = Cookies.get("accessToken");
    const refreshCookieData = Cookies.get("refreshToken");
    
    if (accessCookieData && refreshCookieData) {
        Cookies.remove("accessToken");
        Cookies.remove("refreshToken");
    }
    
    return true;
}

const GetAccessTokenFromCookies = (): string | null => {
    const accessToken = Cookies.get("accessToken");
    return accessToken || null;
};

export const GetUserFromToken = (): UserPayload | null => {
    const token = GetAccessTokenFromCookies();
    if (!token) {
        return null;
    }

    try {
        return jwtDecode<UserPayload>(token);
    } catch (error) {
        console.error("Invalid token", error);
        return null;
    }
};

export const isAuthenticated = () => {
    const accessToken = Cookies.get("accessToken");

    return !!accessToken;
};