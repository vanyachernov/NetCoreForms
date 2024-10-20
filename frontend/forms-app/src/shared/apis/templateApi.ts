import axios from "axios";
import urls from "../constants/urls.ts";

interface TemplateAttributes {
    title: string;
    description: string;
}

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
    attributes: TemplateAttributes;
}

export type TemplateViewModel = Omit<Template & TemplateAttributes, "attributes">;

export const getTemplates: () => Promise<TemplateViewModel[]> = async () => {
    const response = await axios.get<{ data: Template[] }>(urls.FORMS);
    if (response.data.result) {
        const data = response.data.result.map(
            ({ id, owner, title, description }: Template & TemplateAttributes): TemplateViewModel => ({
                id,
                owner: {
                    ...owner,
                    fullName: {
                        ...owner.fullName,
                    }
                },
                attributes: {
                    title: title.value,
                    description: description.value,
                }
            })
        );
        return data;
    } else {
        return [];
    }
};