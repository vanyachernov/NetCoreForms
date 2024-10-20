import { createBrowserRouter, RouterProvider } from "react-router-dom";
import routes from "../shared/constants/routes.ts";
import ApplicationLayout from "./ApplicationLayout.tsx";
import Home from "../shared/features/home/Home.tsx";
import Login from "../shared/features/auth/Login.tsx";
import TemplateList from "../shared/features/templates/list/TemplateList.tsx";

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
                path: routes.LOGIN,
                element: <Login />
            },
            {
                path: routes.TEMPLATES.ROOT,
                element: <TemplateList />
            }
        ]
    },
]);

export default function AppRoutes() {
    return <RouterProvider router={router} fallbackElement={<p>Loading...</p>} />;
}