export const BASE_URL = import.meta.env.VITE_BASE_BACKEND_URL;

const urls = {
    AUTH: {
        REGISTER: `${BASE_URL}/accounts/register`,
        AUTHENTICATE: `${BASE_URL}/accounts/authenticate`,
        REFRESH_TOKEN: `${BASE_URL}/accounts/refresh/:userId`
    },
    FORMS: {
        GET: `${BASE_URL}/templates`,
        GET_BY_ID: `${BASE_URL}/templates/:templateId`
    },
    QUESTIONS: {
        CREATE: `${BASE_URL}/questions/:templateId`,
        GET: `${BASE_URL}/questions/:templateId`
    },
    USERS: {
        GET: `${BASE_URL}/users`,
        GET_USER_TEMPLATES: `${BASE_URL}/users/:userId/templates`,
        CREATE_USER_TEMPLATE: `${BASE_URL}/users/:userId/templates`,
        DELETE_USER_TEMPLATE: `${BASE_URL}/users/:userId/templates/:templateId`
    },
    SALESFORCE: {
        CREATE_CONTACT: `${BASE_URL}/salesforce`,
    }
};

export default urls;