import { jsx as _jsx, jsxs as _jsxs } from "react/jsx-runtime";
import { Button, Heading, Pane, TextInputField, Text, Link, toaster } from "evergreen-ui";
import { useEffect, useState } from "react";
import { Authenticate, isAuthenticated } from "../../apis/authService.ts";
import routes from "../../constants/routes.ts";
import { useLocation, useNavigate } from "react-router-dom";
const Login = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();
    const location = useLocation();
    useEffect(() => {
        if (location.state?.showSuccessToast) {
            toaster.success('Аккаунт', {
                description: 'Ваш аккаунт успешно создан! Войдите в него.',
                duration: 3,
            });
        }
    }, [location.state]);
    useEffect(() => {
        if (isAuthenticated()) {
            navigate(routes.TEMPLATES.ROOT);
        }
    }, [navigate]);
    const handleSubmit = async (e) => {
        e.preventDefault();
        const formData = { email, password };
        try {
            const response = await Authenticate(formData);
            if (response && response.data && response.data.result) {
                navigate(routes.TEMPLATES.ROOT);
            }
        }
        catch (error) {
            console.error("Ошибка авторизации:", error);
        }
    };
    return (_jsx(Pane, { display: "flex", alignItems: "center", justifyContent: "center", paddingTop: 60, children: _jsx(Pane, { display: "flex", flexDirection: "column", width: 350, padding: 10, borderRadius: 8, children: _jsxs(Pane, { background: "gray200", padding: 20, borderRadius: 10, children: [_jsx(Heading, { size: 600, textAlign: "left", marginBottom: 6, children: "\u0410\u0432\u0442\u043E\u0440\u0438\u0437\u0430\u0446\u0438\u044F" }), _jsx(Text, { children: "\u0412\u0432\u0435\u0434\u0456\u0442\u044C \u0441\u0432\u0456\u0439 \u043B\u043E\u0433\u0456\u043D \u0442\u0430 \u043F\u0430\u0440\u043E\u043B\u044C, \u0449\u043E\u0431 \u0443\u0432\u0456\u0439\u0442\u0438 \u0443 \u0441\u0438\u0441\u0442\u0435\u043C\u0443." }), _jsxs("form", { onSubmit: handleSubmit, children: [_jsx(TextInputField, { label: "\u041F\u043E\u0447\u0442\u0430", type: "email", placeholder: "example@gmail.com", value: email, marginTop: 20, onChange: (e) => setEmail(e.target.value) }), _jsx(TextInputField, { label: "\u041F\u0430\u0440\u043E\u043B\u044C", type: "password", placeholder: "\u0412\u0432\u0435\u0434\u0438\u0442\u0435 \u0432\u0430\u0448 \u043F\u0430\u0440\u043E\u043B\u044C", value: password, onChange: (e) => setPassword(e.target.value) }), _jsxs(Text, { display: "flex", justifyContent: "center", alignItems: "center", marginBottom: 10, gap: 6, children: ["\u0423 \u0412\u0430\u0441 \u043D\u0435\u0442 \u0430\u043A\u043A\u0430\u0443\u043D\u0442\u0430?", _jsx(Link, { href: routes.AUTH.SIGN_UP, children: "\u0421\u043E\u0437\u0434\u0430\u0439\u0442\u0435 \u0435\u0433\u043E!" })] }), _jsx(Button, { appearance: "primary", intent: "success", marginTop: 8, onClick: handleSubmit, width: "100%", children: "\u0412\u043E\u0439\u0442\u0438" })] })] }) }) }));
};
export default Login;
