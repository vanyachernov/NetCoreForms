import urls from "../constants/urls.ts";
import axios from "axios";
import Cookies from "js-cookie";

export interface AuthenticateUserRequest {
    email: string;
    password: string;
}

export const Authenticate = async (request: AuthenticateUserRequest) => {
    try {
        const responseAuthenticatedUser = await axios.post(
            `${urls.AUTH.AUTHENTICATE}`,
            request);

        if (responseAuthenticatedUser.data.result)
        {
            Cookies.set(
                "accessToken", 
                JSON.stringify(responseAuthenticatedUser.data.result.accessToken));

            Cookies.set(
                "refreshToken",
                JSON.stringify(responseAuthenticatedUser.data.result.refreshToken));
        }

        return responseAuthenticatedUser;
    } catch (exception) {
        console.error(exception);
        throw exception;
    }
}

export const isAuthenticated = () => {
    const accessToken = Cookies.get("accessToken");

    return !!accessToken;
};