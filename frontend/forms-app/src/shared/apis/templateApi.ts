import axios from "axios";
import urls from "../constants/urls.ts";

interface TemplateAttributes {
    title: string;
    description: string;
}

interface Template {
    id: string;
    attributes: TemplateAttributes;
}

export type TemplateViewModel = Omit<Template & TemplateAttributes, "attributes">;

export const getTemplates: () => Promise<TemplateViewModel[]> = async () => {
    const response = await axios.get<{ data: Template[] }>(urls.FORMS);
    return response.data.data.map(
        (beer: Template): TemplateViewModel => ({
            id: beer.id,
            ...beer.attributes,
        }),
    );
};