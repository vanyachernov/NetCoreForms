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
        element: <ApplicationLayout />,
        children: [
            {
                index: true,
                element: <Home />,
            },
            {
                path: routes.AUTH.SIGN_IN,
                element: <Login />
            },
            {
                path: routes.AUTH.SIGN_UP,
                element: <Register />
            },
            {
                path: routes.TEMPLATES.ROOT,
                element: <TemplateList />
            },
            {
                path: routes.TEMPLATES.EDIT,
                element: <SpecifyTemplate />
            }
        ]
    },
]);

export default function AppRoutes() {
    return <RouterProvider router={router} fallbackElement={<p>Loading...</p>} />;
}