import { jsx as _jsx } from "react/jsx-runtime";
import { Pane } from "evergreen-ui";
import { useEffect, useState } from "react";
import { isAuthenticated } from "../../../apis/authService.ts";
import { useNavigate } from "react-router-dom";
import routes from "../../../constants/routes.ts";
import QuestionSection from "./QuestionSection.tsx";
// So far...
const questions = [
    {}
];
const SpecifyTemplate = () => {
    const [formTitle, setFormTitle] = useState('');
    const [formDescription, setFormDescription] = useState('');
    const navigate = useNavigate();
    useEffect(() => {
        if (!isAuthenticated()) {
            navigate(routes.AUTH.SIGN_IN);
        }
    }, [navigate]);
    return (_jsx(Pane, { display: "flex", height: "100vh", children: _jsx(Pane, { backgroundColor: "#B1D690", paddingLeft: 500, paddingTop: 30, paddingRight: 500, width: "100%", children: questions.map(() => (_jsx(QuestionSection, { id: "1", title: formTitle, description: formDescription, onTitleChange: setFormTitle, onDescriptionChange: setFormDescription }, "1"))) }) }));
};
export default SpecifyTemplate;
