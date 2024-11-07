import urls from "../constants/urls.ts";
import axios from "axios";
import {toaster} from "evergreen-ui";

export const JIRA_BASE_URL = import.meta.env.VITE_JIRA_BASE_URL;

export interface JiraRequest {
    Summary: string;
    Description: string;
    Email: string;
    Priority: string;
    Link: string;
    TemplateName: string | null;
}

export interface TaskViewModel {
    id: string;
    key: string;
    self: string;
    fields: {
        summary: string;
        description: string;
        status: string;
        link: string;
        priority: {
            value: string;
        };
        type: string;
    };
}

export const getTasks = async (email: string): Promise<TaskViewModel[]> => {
    try {
        const response = await axios.get<{ result: TaskViewModel[] }>(`${urls.JIRA.GET_USER_TASKS(email)}`, {
            params: { email }
        });
        if (response.data && response.data.result) {
            return response.data.result.map((task) => ({
                id: task.id,
                key: task.key,
                self: task.self,
                fields: {
                    summary: task.fields.summary,
                    description: task.fields.description,
                    status: task.fields.status,
                    link: task.fields.link,
                    priority: {
                        value: task.fields.priority.value,
                    },
                    type: task.fields.type,
                },
            }));
        }
    } catch (error) {
        console.error("Error fetching tasks:", error);
        return [];
    }
    
    return [];
};


export const CreateAccount = async (request: JiraRequest) => {
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