import { jsx as _jsx, jsxs as _jsxs } from "react/jsx-runtime";
import { Avatar, Button, Heading, Link, Menu, Pane, Popover, Position, Text } from "evergreen-ui";
import routes from "../shared/constants/routes.ts";
import { useNavigate } from "react-router-dom";
import { Deauthenticate, GetUserFromToken, isAuthenticated } from "../shared/apis/authService.ts";
import { roles } from "../shared/logic/roles.ts";
function NavigationPanel() {
    const navigate = useNavigate();
    const userData = GetUserFromToken();
    const handleFormsClick = () => {
        navigate(isAuthenticated()
            ? routes.TEMPLATES.ROOT
            : routes.AUTH.SIGN_IN);
    };
    const handleSignOut = async () => {
        const signOutResult = await Deauthenticate();
        if (signOutResult) {
            navigate(routes.AUTH.SIGN_IN);
        }
    };
    return (_jsxs(Pane, { display: "flex", alignItems: "center", justifyContent: "space-between", padding: 16, background: "tint2", children: [_jsx(Pane, { children: _jsx(Link, { onClick: () => (isAuthenticated())
                        ? navigate(routes.TEMPLATES.ROOT)
                        : navigate(routes.AUTH.SIGN_IN), cursor: "pointer", children: _jsx(Heading, { size: 600, children: "Easy Forms" }) }) }), _jsx(Pane, { display: "flex", alignItems: "center", children: !isAuthenticated() ? (_jsx(Button, { appearance: "default", intent: "success", size: "large", padding: 20, onClick: handleFormsClick, children: "\u041F\u0435\u0440\u0435\u0439\u0442\u0438 \u0432 \u0444\u043E\u0440\u043C\u044B" })) : (_jsx(Popover, { position: Position.BOTTOM_LEFT, content: _jsx(Menu, { children: _jsxs(Menu.Group, { children: [userData?.userRole === roles.ADMIN && (_jsx(Menu.Item, { intent: "access", children: "Admin Panel" })), _jsx(Menu.Item, { intent: "danger", onClick: handleSignOut, children: "Sign out" })] }) }), children: _jsxs(Pane, { display: "flex", alignItems: "center", gap: 10, children: [_jsxs(Text, { children: ["\u041F\u0440\u0438\u0432\u0435\u0442, ", userData?.userFirstname] }), _jsx(Avatar, { name: `${userData?.userFirstname || "Unknown"} ${userData?.userLastname || "Unknown"}`, size: 30 })] }) })) })] }));
}
export default NavigationPanel;
