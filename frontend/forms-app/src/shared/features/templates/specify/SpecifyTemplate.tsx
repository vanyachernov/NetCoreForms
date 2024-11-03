import { Pane } from "evergreen-ui";
import { useEffect, useState } from "react";
import { isAuthenticated } from "../../../apis/authApi.ts";
import { useNavigate } from "react-router-dom";
import routes from "../../../constants/routes.ts";
import QuestionSection from "./QuestionSection.tsx";

// So far...
const questions = [
    {}
];

const SpecifyTemplate = () => {
    const [formTitle, setFormTitle] = useState<string>('');
    const [formDescription, setFormDescription] = useState<string>('');
    
    const navigate = useNavigate();

    useEffect(() => {
        if (!isAuthenticated()) {
            navigate(routes.AUTH.SIGN_IN);
        }
    }, [navigate]);

    return (
        <Pane display="flex" 
              height="100vh">
            <Pane
                backgroundColor="#B1D690"
                paddingLeft={500}
                paddingTop={30}
                paddingRight={500}
                width="100%"
            >
                {questions.map(() => (
                    <QuestionSection
                        key="1"
                        id="1"
                        title={formTitle}
                        description={formDescription}
                        onTitleChange={setFormTitle}
                        onDescriptionChange={setFormDescription}
                    />
                ))}
            </Pane>
        </Pane>
    );
};

export default SpecifyTemplate;
