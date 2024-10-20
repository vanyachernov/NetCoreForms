import axios from "axios";
import urls from "../constants/urls.ts";

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

export const getTemplates: () => Promise<TemplateViewModel[]> = async () => {
    try {
        const response = await axios.get<{ result: Template[] }>(urls.FORMS.GET);
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

export const getTemplatesById: (id: string) => Promise<TemplateViewModel[]> = async (id) => {
    try {
        const url = urls.FORMS.GET_BY_ID.replace(":templateId", id);
        const response = await axios.get<{ result: Template[] }>(url);
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