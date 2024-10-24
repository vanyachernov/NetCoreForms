import axios from "axios";
import urls from "../constants/urls.ts";
import { GetUserFromToken } from "./authService.ts";
export const getTemplates = async (token) => {
    try {
        const config = {
            headers: {
                Authorization: `Bearer ${token}`
            }
        };
        const response = await axios.get(urls.FORMS.GET, config);
        if (response.data.result) {
            return response.data.result.map(({ id, owner, title, description }) => ({
                id,
                owner: {
                    ...owner,
                    fullName: {
                        ...owner.fullName,
                    }
                },
                title: title?.value,
                description: description?.value
            }));
        }
        return [];
    }
    catch (error) {
        console.error("Error fetching templates:", error);
        return [];
    }
};
export const getTemplatesById = async (id, token) => {
    try {
        const url = urls.USERS.GET_USER_TEMPLATES.replace(":userId", id);
        const config = {
            headers: {
                Authorization: `Bearer ${token}`
            }
        };
        const response = await axios.get(url, config);
        if (response.data.result) {
            return response.data.result.map(({ id, owner, title, description }) => ({
                id,
                owner: {
                    ...owner,
                    fullName: {
                        ...owner.fullName,
                    }
                },
                title: title?.value,
                description: description?.value
            }));
        }
        return [];
    }
    catch (error) {
        console.error("Error fetching templates:", error);
        return [];
    }
};
export const createTemplate = async (id, data, token) => {
    try {
        const url = urls.USERS.CREATE_USER_TEMPLATE.replace(":userId", id);
        const body = {
            title: { value: data.title },
            description: { value: data.description }
        };
        const config = {
            headers: {
                Authorization: `Bearer ${token}`
            }
        };
        const response = await axios.post(url, body, config);
        if (response.data.result) {
            return response.data.result;
        }
        return null;
    }
    catch (error) {
        console.error("Error creating template:", error);
        return null;
    }
};
export const deleteTemplate = async (id, token) => {
    const user = GetUserFromToken();
    if (user === null) {
        return false;
    }
    const url = urls.USERS.DELETE_USER_TEMPLATE
        .replace(":userId", user.userId)
        .replace(":templateId", id);
    const config = {
        headers: {
            Authorization: `Bearer ${token}`
        }
    };
    await axios.delete(url, config);
    return true;
};
