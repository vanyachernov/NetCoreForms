import { Pane } from 'evergreen-ui';
import {Outlet} from "react-router-dom";
import NavigationPanel from "./NavigationPanel.tsx";
import JiraIconButton from "./JiraIconButton.tsx";
import {useEffect} from "react";
import {useAuth} from "./AuthContext";

const ApplicationLayout = () => {
    const { isAuth, updateAuthStatus } = useAuth();

    useEffect(() => {
        updateAuthStatus();
    }, [updateAuthStatus]);
    
    
    return (
        <Pane 
            display="flex" 
            flexDirection="column" 
            minHeight="100vh">
            
            <NavigationPanel />
            
            <Pane flex="1">
                <Outlet />
                {isAuth ? <JiraIconButton /> : null}
            </Pane>
            
            <Pane 
                padding={16} 
                background="tint2" 
                textAlign="center"
                fontSize="small">
                © 2024 Easy Forms. Все права защищены.
            </Pane>
            
        </Pane>
    );
};

export default ApplicationLayout;