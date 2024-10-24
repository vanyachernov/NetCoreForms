import { jsx as _jsx, jsxs as _jsxs } from "react/jsx-runtime";
import { Button, Heading, Link, Pane, Text, TextInputField } from "evergreen-ui";
import { useEffect, useState } from "react";
import { isAuthenticated, Register } from "../../apis/authService.ts";
import routes from "../../constants/routes.ts";
import { useNavigate } from "react-router-dom";
const Login = () => {
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();
    useEffect(() => {
        if (isAuthenticated()) {
            navigate(routes.TEMPLATES.ROOT);
        }
    }, []);
    const handleSubmit = async (e) => {
        e.preventDefault();
        const formData = {
            fullName: {
                lastName,
                firstName
            },
            email: {
                email
            },
            password: {
                password
            }
        };
        try {
            const response = await Register(formData);
            if (response) {
                navigate(routes.AUTH.SIGN_IN, { state: { showSuccessToast: true } });
            }
        }
        catch (error) {
            console.error("Ошибка авторизации:", error);
        }
    };
    return (_jsx(Pane, { display: "flex", alignItems: "center", justifyContent: "center", paddingTop: 60, children: _jsx(Pane, { display: "flex", flexDirection: "column", width: 350, padding: 24, borderRadius: 8, children: _jsxs(Pane, { background: "gray200", padding: 20, borderRadius: 10, children: [_jsx(Heading, { size: 600, textAlign: "left", marginBottom: 6, children: "\u0420\u0435\u0433\u0438\u0441\u0442\u0440\u0430\u0446\u0438\u044F" }), _jsx(Text, { children: "\u0412\u0432\u0435\u0434\u0438\u0442\u0435 \u0432\u0441\u0435 \u043D\u0435\u043E\u0431\u0445\u043E\u0434\u0438\u043C\u044B\u0435 \u0434\u0430\u043D\u043D\u044B\u0435, \u0447\u0442\u043E\u0431\u044B \u0438\u043C\u0435\u0442\u044C \u0434\u043E\u0441\u0442\u0443\u043F \u043A \u0441\u0438\u0441\u0442\u0435\u043C\u0435." }), _jsxs("form", { onSubmit: handleSubmit, children: [_jsx(TextInputField, { label: "\u0418\u043C\u044F", type: "text", placeholder: "\u041F\u0440\u0438\u043C\u0435\u0440: \u0418\u0432\u0430\u043D", required: true, value: firstName, marginTop: 20, onChange: (e) => setFirstName(e.target.value) }), _jsx(TextInputField, { label: "\u0424\u0430\u043C\u0438\u043B\u0438\u044F", type: "text", placeholder: "\u041F\u0440\u0438\u043C\u0435\u0440: \u041F\u0435\u0442\u0440\u043E\u0432", required: true, value: lastName, onChange: (e) => setLastName(e.target.value) }), _jsx(TextInputField, { label: "\u041B\u043E\u0433\u0438\u043D", type: "email", placeholder: "example@gmail.com", required: true, value: email, onChange: (e) => setEmail(e.target.value) }), _jsx(TextInputField, { label: "\u041F\u0430\u0440\u043E\u043B\u044C", type: "password", placeholder: "\u041F\u0430\u0440\u043E\u043B\u044C", required: true, value: password, onChange: (e) => setPassword(e.target.value) }), _jsxs(Text, { display: "flex", justifyContent: "center", alignItems: "center", marginBottom: 10, gap: 6, children: ["\u0423 \u0412\u0430\u0441 \u0443\u0436\u0435 \u0435\u0441\u0442\u044C \u0430\u043A\u043A\u0430\u0443\u043D\u0442?", _jsx(Link, { href: routes.AUTH.SIGN_IN, children: "\u0412\u043E\u0439\u0434\u0438\u0442\u0435!" })] }), _jsx(Button, { appearance: "primary", intent: "success", marginTop: 5, onClick: handleSubmit, width: "100%", children: "\u0417\u0430\u0440\u0435\u0433\u0438\u0441\u0442\u0440\u0438\u0440\u043E\u0432\u0430\u0442\u044C\u0441\u044F" })] })] }) }) }));
};
export default Login;
