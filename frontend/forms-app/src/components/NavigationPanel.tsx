import {Button, Heading, Pane} from "evergreen-ui";

function NavigationPanel() {
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
                    intent="none">
                    Перейти в формы
                </Button>
            </Pane>
        </Pane>
    );
}

export default NavigationPanel;