import {Button, Heading, Pane} from "evergreen-ui";
import routes from "../shared/constants/routes.ts";
import {useNavigate} from "react-router-dom";
import {isAuthenticated} from "../shared/apis/authService.ts";

function NavigationPanel() {
    const navigate = useNavigate();

    const handleFormsClick = () => {
        navigate(isAuthenticated() 
            ? routes.TEMPLATES.ROOT 
            : routes.LOGIN);
    };
    
    return (
        <Pane 
            display="flex" 
            alignItems="center"
            justifyContent="space-between" 
            padding={16} 
            background="tint2">
            <Pane>
                <Heading size={600}>Easy Forms</Heading>
            </Pane>
            <Pane>
                <Button 
                    appearance="default"
                    color="blue"
                    size="large"
                    padding={20}
                    intent="none"
                    onClick={handleFormsClick}>
                    Перейти в формы
                </Button>
            </Pane>
        </Pane>
    );
}

export default NavigationPanel;