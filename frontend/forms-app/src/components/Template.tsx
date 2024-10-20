import {Avatar, CogIcon, Icon, Link, Menu, Pane, Popover, Position, Text} from "evergreen-ui";
import {roles} from "../shared/logic/roles.ts";

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
            <Pane 
                display="flex"
                justifyContent="space-between"
                alignItems="center">
                <Text
                    size={300}
                    color="muted">
                    Автор: {template.owner.fullName.firstName} {template.owner.fullName.lastName}
                </Text>
                <Popover
                    position={Position.BOTTOM_LEFT}
                    content={
                        <Menu>
                            <Menu.Group>
                                <Menu.Item intent="access">
                                    Переименование
                                </Menu.Item>
                                <Menu.Item intent="access">
                                    Открытие новой вкладки
                                </Menu.Item>
                                <Menu.Item intent="danger">
                                    Удалить
                                </Menu.Item>
                            </Menu.Group>
                        </Menu>
                    }>
                    <Icon
                        icon={CogIcon}
                        size={12}
                        cursor="pointer"
                    />
                </Popover>
                
            </Pane>
        </Pane>
    );
};

export default Template;