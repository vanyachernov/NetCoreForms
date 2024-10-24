import { jsx as _jsx, jsxs as _jsxs } from "react/jsx-runtime";
import { useEffect, useState } from "react";
import { createTemplate, deleteTemplate, getTemplates, getTemplatesById } from "../../../apis/templateApi.ts";
import { Heading, Pane, Text, toaster } from "evergreen-ui";
import Template from "../../../../components/Template.tsx";
import Spinner from "../../../../components/Spinner.tsx";
import { GetAccessTokenFromCookies, GetUserFromToken, isAuthenticated } from "../../../apis/authService.ts";
import { roles } from "../../../logic/roles.ts";
import EmptyStateLayout from "../../../../components/EmptyStateLayout.tsx";
import { useNavigate } from "react-router-dom";
import routes from "../../../constants/routes.ts";
const TemplateList = () => {
    const [templates, setTemplates] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const user = GetUserFromToken();
    const navigate = useNavigate();
    useEffect(() => {
        const fetchData = async () => {
            try {
                if (user === null) {
                    navigate(routes.AUTH.SIGN_IN);
                    return;
                }
                const accessToken = GetAccessTokenFromCookies();
                let responseData = null;
                if (user.userRole === roles.ADMIN) {
                    responseData = await getTemplates(accessToken);
                }
                else {
                    responseData = await getTemplatesById(user.userId.toString(), accessToken);
                }
                setTemplates(responseData);
            }
            catch (error) {
                console.error("Error fetching templates:", error);
            }
            finally {
                setIsLoading(false);
            }
        };
        fetchData();
    }, []);
    const handleCreateEmptyTemplate = async () => {
        if (!isAuthenticated()) {
            navigate(routes.AUTH.SIGN_IN);
            return;
        }
        if (!user?.userId) {
            console.error("User identifier is undefined!");
            return;
        }
        const accessToken = GetAccessTokenFromCookies();
        try {
            const templateData = {
                title: "Мой новый шаблон",
                description: "Описание для нового шаблона"
            };
            const createdTemplateIdentifier = await createTemplate(user?.userId, templateData, accessToken);
            if (createdTemplateIdentifier) {
                const editPath = routes.TEMPLATES.EDIT.replace(':id', createdTemplateIdentifier);
                navigate(editPath);
            }
            else {
                toaster.danger('Создание шаблона', {
                    description: 'Во время создания шаблона возникла ошибка.',
                    duration: 5,
                });
            }
        }
        catch (error) {
            console.error("Error creating template:", error);
        }
    };
    const handleDeleteTemplate = async (templateId) => {
        try {
            const accessToken = GetAccessTokenFromCookies();
            const deleteTemplateStatus = await deleteTemplate(templateId, accessToken);
            if (deleteTemplateStatus) {
                toaster.success('Удаление шаблона', {
                    description: 'Шаблон был успешно удален!',
                    duration: 3,
                });
                setTemplates((prevTemplates) => prevTemplates.filter(template => template.id !== templateId));
            }
        }
        catch (error) {
            console.error(error);
        }
    };
    if (isLoading) {
        return _jsx(Spinner, {});
    }
    return (_jsxs(Pane, { children: [_jsx(Pane, { display: "flex", backgroundColor: "#C1D7C9", flexDirection: "column", padding: 20, children: _jsxs(Pane, { children: [_jsx(Heading, { size: 500, fontWeight: 500, marginBottom: 16, marginLeft: 10, marginTop: 6, children: "\u0421\u043E\u0437\u0434\u0430\u0442\u044C \u0444\u043E\u0440\u043C\u0443" }), _jsxs(Pane, { display: "flex", flexDirection: "column", alignItems: "left", marginLeft: 10, children: [_jsx(Pane, { display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "center", width: 160, height: 120, border: "solid", borderWidth: 0.1, borderColor: "green", borderRadius: 8, cursor: "pointer", onClick: handleCreateEmptyTemplate, backgroundColor: "white", children: _jsx(Pane, { width: 50, height: 50, backgroundImage: "url('https://cdn-icons-png.flaticon.com/512/2997/2997933.png')", backgroundSize: "contain", backgroundPosition: "center", backgroundRepeat: "no-repeat" }) }), _jsx(Text, { size: 400, fontWeight: 500, marginTop: 6, marginLeft: 6, children: "\u041F\u0443\u0441\u0442\u0430\u044F \u0444\u043E\u0440\u043C\u0430" })] })] }) }), _jsx(Pane, { display: "flex", flexDirection: "column", padding: 10, children: _jsxs(Pane, { marginLeft: 10, marginTop: 10, children: [_jsx(Heading, { size: 500, fontWeight: 500, marginBottom: 16, marginLeft: 10, marginTop: 6, children: "\u041D\u0435\u0434\u0430\u0432\u043D\u0438\u0435 \u0448\u0430\u0431\u043B\u043E\u043D\u044B" }), templates.length > 0 ? (_jsx(Pane, { display: "flex", alignItems: "left", justifyContent: "left", children: _jsx(Pane, { display: "flex", flexDirection: "row", flexWrap: "wrap", alignItems: "center", children: templates.map((template, index) => (_jsx(Template, { template: template, handleDeleteTemplate: handleDeleteTemplate }, index))) }) })) : (_jsx(Pane, { display: "flex", justifyContent: "center", alignItems: "center", height: "100vh", children: _jsx(EmptyStateLayout, { title: "\u0424\u043E\u0440\u043C \u043D\u0435\u0442!", description: "\u0427\u0442\u043E\u0431\u044B \u043D\u0430\u0447\u0430\u0442\u044C, \u0432\u044B\u0431\u0435\u0440\u0438\u0442\u0435 \u043F\u0443\u0441\u0442\u0443\u044E \u0444\u043E\u0440\u043C\u0443 \u0438\u043B\u0438 \u043E\u0434\u0438\u043D \u0438\u0437 \u043D\u0435\u0434\u0430\u0432\u043D\u0438\u0445 \u0448\u0430\u0431\u043B\u043E\u043D\u043E\u0432.", tip: "" }) }))] }) })] }));
};
export default TemplateList;
