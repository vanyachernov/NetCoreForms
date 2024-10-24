import { jsx as _jsx } from "react/jsx-runtime";
import { EmptyState, SearchIcon } from "evergreen-ui";
import urls from "../shared/constants/urls.ts";
const EmptyStateLayout = ({ title, description, tip }) => {
    return (_jsx(EmptyState, { background: "light", title: title, orientation: "horizontal", icon: _jsx(SearchIcon, { color: "#C1C4D6" }), iconBgColor: "#EDEFF5", description: description, anchorCta: _jsx(EmptyState.LinkButton, { href: urls.USERS.CREATE_USER_TEMPLATE, target: "_blank", children: tip }) }));
};
export default EmptyStateLayout;
