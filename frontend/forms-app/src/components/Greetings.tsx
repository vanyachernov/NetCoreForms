import {Button, Heading, Pane, Text} from "evergreen-ui";

const Greetings = () => {
    return (
        <Pane display="flex" flexDirection="column" alignItems="flex-start">
            <Heading 
                size={900} 
                fontSize={60} 
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
                height={48}>
                Попробовать
            </Button>
        </Pane>
    );
};

export default Greetings;