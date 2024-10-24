import { jsx as _jsx, jsxs as _jsxs } from "react/jsx-runtime";
import { Pane } from 'evergreen-ui';
import { Outlet } from "react-router-dom";
import NavigationPanel from "./NavigationPanel.tsx";
const ApplicationLayout = () => {
    return (_jsxs(Pane, { display: "flex", flexDirection: "column", minHeight: "100vh", children: [_jsx(NavigationPanel, {}), _jsx(Pane, { flex: "1", children: _jsx(Outlet, {}) }), _jsx(Pane, { padding: 16, background: "tint2", textAlign: "center", fontSize: "small", children: "\u00A9 2024 Easy Forms. \u0412\u0441\u0435 \u043F\u0440\u0430\u0432\u0430 \u0437\u0430\u0449\u0438\u0449\u0435\u043D\u044B." })] }));
};
export default ApplicationLayout;
