import {Pane, Textarea, TextInputField} from "evergreen-ui";
import {useEffect, useState} from "react";
import {GetUserFromToken, isAuthenticated} from "../../../apis/authService.ts";
import {useNavigate} from "react-router-dom";
import routes from "../../../constants/routes.ts";

const SpecifyTemplate = () => {
    const [formTitle, setFormTitle] = useState<string>('');
    const [formDescription, setFormDescription] = useState<string>('');

    const user = GetUserFromToken();
    const navigate = useNavigate();

    useEffect(() => {
        if (!isAuthenticated()) {
            navigate(routes.AUTH.SIGN_IN);
        }
    }, [navigate]);

    useEffect(() => {
        
    }, []);
    
    return (
        <Pane padding={24}>
            <Pane marginBottom={24}>
                <TextInputField
                    label="Название формы"
                    placeholder="Введите название формы"
                    value={formTitle}
                    onChange={(e) => setFormTitle(e.target.value)}
                />
                <Textarea
                    placeholder="Описание формы"
                    value={formDescription}
                    onChange={(e) => setFormDescription(e.target.value)}
                />
            </Pane>
        </Pane>
    );
};

export default SpecifyTemplate;