import { Pane } from 'evergreen-ui';
import {Outlet} from "react-router-dom";
import NavigationPanel from "./NavigationPanel.tsx";

const ApplicationLayout = () => {
    return (
        <Pane 
            display="flex" 
            flexDirection="column" 
            minHeight="100vh">
            
            <NavigationPanel />
            
            <Pane flex="1" padding={16}>
                <Outlet />
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