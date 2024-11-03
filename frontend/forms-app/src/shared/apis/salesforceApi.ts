import axios from "axios";
import urls from "../constants/urls.ts";
import {toaster} from "evergreen-ui";

export interface SalesforceRequest {
    AccountName: string;
    FirstName: string;
    LastName: string;
    Email: string;
}

export const CreateAccount = async (request: SalesforceRequest) => {
    try {
        const response = await axios.post(
            `${urls.SALESFORCE.CREATE_CONTACT}`, 
            request);
        
        if (response.data && response.data.result) {
            toaster.success('Аккаунт', {
                description: 'Аккаунт успешно создан.',
                duration: 5
            });
            return response;
        } 
        if (response.data && response.data.errors)
        {
            return null;
        }
    } catch {
        toaster.danger('Дублирование', {
            description: 'Ваш аккаунт уже зарегистрирован в сервисе Salesforce.',
            duration: 5
        });
        return null;
    }
};