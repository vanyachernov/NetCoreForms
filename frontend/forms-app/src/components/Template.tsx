import {Link, Pane, Text} from "evergreen-ui";

const Template = ({template}) => {
    return (
        <Pane
            display="flex"
            flexDirection="column"
            border="default"
            borderRadius={8}
            padding={16}
            margin={8}
            width={200}
            elevation={2}
            hoverElevation={3}
        >
            <Link marginBottom={8} cursor="pointer">
                <Text 
                    size={400} 
                    fontWeight={700}>
                    {template.title}
                </Text>
            </Link>
            <Text 
                fontSize={12} 
                marginBottom={8}>
                {template.description}
            </Text>
            <Text 
                size={300} 
                color="muted">
                Автор: {template.owner.fullName.firstName} {template.owner.fullName.lastName}
            </Text>
        </Pane>
    );
};

export default Template;