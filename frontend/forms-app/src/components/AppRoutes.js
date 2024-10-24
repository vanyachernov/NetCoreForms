import { jsx as _jsx } from "react/jsx-runtime";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import routes from "../shared/constants/routes.ts";
import ApplicationLayout from "./ApplicationLayout.tsx";
import Home from "../shared/features/home/Home.tsx";
import Login from "../shared/features/auth/Login.tsx";
import TemplateList from "../shared/features/templates/list/TemplateList.tsx";
import Register from "../shared/features/auth/Register.tsx";
import SpecifyTemplate from "../shared/features/templates/specify/SpecifyTemplate.tsx";
const router = createBrowserRouter([
    {
        path: routes.HOME,
        element: _jsx(ApplicationLayout, {}),
        children: [
            {
                index: true,
                element: _jsx(Home, {}),
            },
            {
                path: routes.AUTH.SIGN_IN,
                element: _jsx(Login, {})
            },
            {
                path: routes.AUTH.SIGN_UP,
                element: _jsx(Register, {})
            },
            {
                path: routes.TEMPLATES.ROOT,
                element: _jsx(TemplateList, {})
            },
            {
                path: routes.TEMPLATES.EDIT,
                element: _jsx(SpecifyTemplate, {})
            }
        ]
    },
]);
export default function AppRoutes() {
    return _jsx(RouterProvider, { router: router, fallbackElement: _jsx("p", { children: "Loading..." }) });
}
