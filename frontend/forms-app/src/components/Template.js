import { jsx as _jsx, jsxs as _jsxs } from "react/jsx-runtime";
import { CogIcon, Dialog, Icon, Link, Menu, Pane, Popover, Position, Text } from "evergreen-ui";
import routes from "../shared/constants/routes.ts";
import { useNavigate } from "react-router-dom";
import { useState } from "react";
const truncateText = (text, maxLength) => {
    if (text.length > maxLength) {
        return text.substring(0, maxLength) + '...';
    }
    return text;
};
const Template = ({ template, handleDeleteTemplate }) => {
    const [isDeleteDialogShown, setIsDeleteDialogShown] = useState(false);
    const truncatedTitle = truncateText(template.title, 30);
    const truncatedDescription = truncateText(template.description, 60);
    const navigate = useNavigate();
    const handleDelete = async () => {
        handleDeleteTemplate(template.id);
        setIsDeleteDialogShown(false);
    };
    return (_jsxs(Pane, { display: "flex", flexDirection: "column", border: "default", borderRadius: 8, padding: 16, margin: 8, width: 280, elevation: 2, hoverElevation: 3, background: "tint1", children: [_jsx(Link, { onClick: () => navigate(routes.TEMPLATES.EDIT.replace(':id', template.id)), marginBottom: 8, cursor: "pointer", children: _jsx(Text, { size: 500, fontWeight: 500, color: "green600", textOverflow: "ellipsis", children: truncatedTitle }) }), _jsx(Text, { fontSize: 14, marginBottom: 12, color: "muted", textOverflow: "ellipsis", children: truncatedDescription }), _jsxs(Pane, { display: "flex", justifyContent: "space-between", alignItems: "center", children: [_jsxs(Text, { size: 300, color: "muted", children: ["\u0410\u0432\u0442\u043E\u0440: ", template.owner.fullName.firstName, " ", template.owner.fullName.lastName] }), _jsx(Popover, { position: Position.BOTTOM_LEFT, content: _jsx(Menu, { children: _jsxs(Menu.Group, { children: [_jsx(Menu.Item, { intent: "access", onClick: () => window.open(routes.TEMPLATES.EDIT.replace(':id', template.id), '_blank'), children: "\u041E\u0442\u043A\u0440\u044B\u0442\u044C \u0432 \u043D\u043E\u0432\u043E\u0439 \u0432\u043A\u043B\u0430\u0434\u043A\u0435" }), _jsx(Menu.Item, { intent: "danger", onClick: () => setIsDeleteDialogShown(true), children: "\u0423\u0434\u0430\u043B\u0438\u0442\u044C" })] }) }), children: _jsx(Icon, { icon: CogIcon, size: 16, cursor: "pointer" }) })] }), _jsx(Dialog, { isShown: isDeleteDialogShown, title: "\u0423\u0434\u0430\u043B\u0435\u043D\u0438\u0435 \u0448\u0430\u0431\u043B\u043E\u043D\u0430", intent: "danger", onCloseComplete: () => setIsDeleteDialogShown(false), onConfirm: handleDelete, confirmLabel: "\u0423\u0434\u0430\u043B\u0438\u0442\u044C", children: "\u0412\u044B \u0443\u0432\u0435\u0440\u0435\u043D\u044B, \u0447\u0442\u043E \u0445\u043E\u0442\u0438\u0442\u0435 \u0443\u0434\u0430\u043B\u0438\u0442\u044C \u0448\u0430\u0431\u043B\u043E\u043D?" })] }));
};
export default Template;
