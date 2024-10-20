import {Button, Heading, Pane, Text} from "evergreen-ui";
import {useNavigate} from "react-router-dom";
import routes from "../shared/constants/routes.ts";
import {isAuthenticated} from "../shared/apis/authService.ts";

const Greetings = () => {
    const navigate = useNavigate();

    const handleRouteToTemplate = () => {
        navigate(isAuthenticated()
            ? routes.TEMPLATES.ROOT
            : routes.LOGIN);
    };
    
    return (
        <Pane display="flex" flexDirection="column" alignItems="flex-start">
            <Heading 
                size={900} 
                fontSize={50} 
                lineHeight={1.15} 
                marginBottom={16}>
                Составляйте шаблоны и получайте информацию с помощью Easy Forms
            </Heading>
            <Text 
                size={600} 
                marginBottom={24}>
                Создавайте опросы, собирайте данные и анализируйте их с лёгкостью с нашим удобным сервисом.
            </Text>
            <Button 
                appearance="primary" 
                height={48}
                onClick={handleRouteToTemplate}>
                Попробовать
            </Button>
        </Pane>
    );
};

export default Greetings;