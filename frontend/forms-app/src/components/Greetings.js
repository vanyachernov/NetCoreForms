import { jsx as _jsx, jsxs as _jsxs } from "react/jsx-runtime";
import { Button, Heading, Pane, Text } from "evergreen-ui";
import { useNavigate } from "react-router-dom";
import routes from "../shared/constants/routes.ts";
import { isAuthenticated } from "../shared/apis/authService.ts";
const Greetings = () => {
    const navigate = useNavigate();
    const handleRouteToTemplate = () => {
        navigate(isAuthenticated()
            ? routes.TEMPLATES.ROOT
            : routes.AUTH.SIGN_IN);
    };
    return (_jsxs(Pane, { display: "flex", flexDirection: "column", alignItems: "flex-start", children: [_jsx(Heading, { size: 900, fontSize: 50, lineHeight: 1.15, marginBottom: 16, children: "\u0421\u043E\u0441\u0442\u0430\u0432\u043B\u044F\u0439\u0442\u0435 \u0448\u0430\u0431\u043B\u043E\u043D\u044B \u0438 \u043F\u043E\u043B\u0443\u0447\u0430\u0439\u0442\u0435 \u0438\u043D\u0444\u043E\u0440\u043C\u0430\u0446\u0438\u044E \u0441 \u043F\u043E\u043C\u043E\u0449\u044C\u044E Easy Forms" }), _jsx(Text, { size: 600, marginBottom: 24, children: "\u0421\u043E\u0437\u0434\u0430\u0432\u0430\u0439\u0442\u0435 \u043E\u043F\u0440\u043E\u0441\u044B, \u0441\u043E\u0431\u0438\u0440\u0430\u0439\u0442\u0435 \u0434\u0430\u043D\u043D\u044B\u0435 \u0438 \u0430\u043D\u0430\u043B\u0438\u0437\u0438\u0440\u0443\u0439\u0442\u0435 \u0438\u0445 \u0441 \u043B\u0451\u0433\u043A\u043E\u0441\u0442\u044C\u044E \u0441 \u043D\u0430\u0448\u0438\u043C \u0443\u0434\u043E\u0431\u043D\u044B\u043C \u0441\u0435\u0440\u0432\u0438\u0441\u043E\u043C." }), _jsx(Button, { appearance: "primary", height: 48, onClick: handleRouteToTemplate, children: "\u041F\u043E\u043F\u0440\u043E\u0431\u043E\u0432\u0430\u0442\u044C" })] }));
};
export default Greetings;
