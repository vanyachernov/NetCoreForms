import urls from "../constants/urls.ts";
import axios from "axios";
import {toaster} from "evergreen-ui";

export interface JiraService {
    summary: string;
    description: string;
    email: string;
    currentUrl: string;
}

export const CreateAccount = async (request: JiraService) => {
    try {
        const response = await axios.post(
            `${urls.JIRA.CREATE_SERVICE}`,
            request);

        if (response.data && response.data.result) {
            toaster.success('Jira', {
                description: `Задание ${response.data.result.key} успешно создано!`,
                duration: 5
            });
            return response;
        }
        if (response.data && response.data.errors)
        {
            return null;
        }
    } catch {
        toaster.danger('Jira', {
            description: 'Произошла ошибка создания задания.',
            duration: 5
        });
        return null;
    }
};