import axios from "axios";
import urls from "../constants/urls.ts";
import {GetUserFromToken} from "./authService.ts";

interface Template {
    id: string;
    owner: {
        id: string;
        email: string;
        fullName: {
            firstName: string;
            lastName: string;
        };
    };
    title: {
        value: string;
    };
    description: {
        value: string;
    };
}

export interface TemplateViewModel {
    id: string;
    owner: {
        id: string;
        email: string;
        fullName: {
            firstName: string;
            lastName: string;
        };
    };
    title: string;
    description: string;
}

export interface CreateTemplate {
    title: string;
    description: string;
}

export const getTemplates: (token: string) => Promise<TemplateViewModel[]> = async (token) => {
    try {
        const config = {
            headers: {
                Authorization: `Bearer ${token}`
            }
        }
        
        const response = await axios.get<{ result: Template[] }>(
            urls.FORMS.GET,
            config);
        
        if (response.data.result) {
            return response.data.result.map(
                ({id, owner, title, description}: Template): TemplateViewModel => ({
                    id,
                    owner: {
                        ...owner,
                        fullName: {
                            ...owner.fullName,
                        }
                    },
                    title: title?.value,
                    description: description?.value
                })
            );
        }
        
        return [];
    } catch (error) {
        console.error("Error fetching templates:", error);
        
        return [];
    }
};

export const getTemplatesById: (
    id: string, 
    token: string) => Promise<TemplateViewModel[]> = async (
        id, 
        token) => {
    try {
        const url = urls.USERS.GET_USER_TEMPLATES.replace(":userId", id);

        const config = {
            headers: {
                Authorization: `Bearer ${token}`
            }
        }
        
        const response = await axios.get<{ result: Template[] }>(
            url, 
            config);
        
        if (response.data.result) {
            return response.data.result.map(
                ({id, owner, title, description}: Template): TemplateViewModel => ({
                    id,
                    owner: {
                        ...owner,
                        fullName: {
                            ...owner.fullName,
                        }
                    },
                    title: title?.value,
                    description: description?.value
                })
            );
        }
        
        return [];
    } catch (error) {
        console.error("Error fetching templates:", error);
        
        return [];
    }
}

export const createTemplate: (
    id: string, 
    data: CreateTemplate,
    token: string) => Promise<string | null> = async (
        id, 
        data, 
        token) => {
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
        }
        
        const response = await axios.post<{result: string}>(
            url, 
            body, 
            config);
        
        if (response.data.result) {
            return response.data.result;
        }
        
        return null;
    } catch (error) {
        console.error("Error creating template:", error);
        
        return null;
    }
}

export const deleteTemplate: (
    id: string,
    token: string) => Promise<boolean> = async (
        id,
        token) =>
{
    const user = GetUserFromToken();
    
    if (user === null)
    {
        return false;
    }
    
    const url = urls.USERS.DELETE_USER_TEMPLATE
        .replace(":userId", user.userId)
        .replace(":templateId", id);

    const config = {
        headers: {
            Authorization: `Bearer ${token}`
        }
    }

    await axios.delete<{result: string}>(
       url,
       config);
    
    return true;
}