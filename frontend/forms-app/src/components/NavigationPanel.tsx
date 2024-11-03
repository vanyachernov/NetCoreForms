import {Avatar, Button, Heading, Link, Menu, Pane, Popover, Position, Text} from "evergreen-ui";
import routes from "../shared/constants/routes.ts";
import {useNavigate} from "react-router-dom";
import {Deauthenticate, GetUserFromToken, isAuthenticated} from "../shared/apis/authService.ts";
import {roles} from "../shared/logic/roles.ts";

function NavigationPanel() {
    const navigate = useNavigate();
    const userData = GetUserFromToken();

    const handleFormsClick = () => {
        navigate(isAuthenticated() 
            ? routes.TEMPLATES.ROOT 
            : routes.AUTH.SIGN_IN);
    };
    
    const handleSignOut = async () => {
        const signOutResult = await Deauthenticate();
        
        if (signOutResult) {
            navigate(routes.AUTH.SIGN_IN);
        }
    };
    
    return (
        <Pane 
            display="flex" 
            alignItems="center"
            justifyContent="space-between" 
            padding={16} 
            background="tint2">
            <Pane>
                <Link onClick={() => (isAuthenticated()) 
                    ? navigate(routes.TEMPLATES.ROOT) 
                    : navigate(routes.AUTH.SIGN_IN)}
                    cursor="pointer">
                    <Heading size={600}>Easy Forms</Heading>
                </Link>
            </Pane>
            <Pane 
                display="flex" 
                alignItems="center">
                {!isAuthenticated() ? (
                    <Button
                        appearance="default"
                        intent="success"
                        size="large"
                        padding={20}
                        onClick={handleFormsClick}>
                        Перейти в формы
                    </Button>  
                ) : (
                    <Popover
                        position={Position.BOTTOM_LEFT}
                        content={
                            <Menu>
                                <Menu.Group>
                                    {userData?.userRole === roles.ADMIN && (
                                        <Menu.Item
                                            intent="access">
                                            Admin Panel
                                        </Menu.Item>
                                    )}
                                    <Menu.Item 
                                        intent="danger"
                                        onClick={handleSignOut}>
                                        Выйти
                                    </Menu.Item>
                                </Menu.Group>
                            </Menu>
                        }>
                        <Pane
                            display="flex"
                            alignItems="center"
                            gap={10}>
                            <Text>
                                Привет, {userData?.userFirstname}
                            </Text>
                            <Avatar
                                name={`${userData?.userFirstname || "Unknown"} ${userData?.userLastname || "Unknown"}`}
                                size={30} />
                        </Pane>
                    </Popover>
                )}
            </Pane>
        </Pane>
    );
}

export default NavigationPanel;