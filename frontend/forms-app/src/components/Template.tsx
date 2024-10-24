import {CogIcon, Dialog, Icon, Link, Menu, Pane, Popover, Position, Text} from "evergreen-ui";
import {TemplateViewModel} from "../shared/apis/templateApi.ts";
import routes from "../shared/constants/routes.ts";
import {useNavigate} from "react-router-dom";
import {useState} from "react";

interface TemplateProps {
    template: TemplateViewModel;
    handleDeleteTemplate: (id: string) => void;
}

const truncateText = (text: string, maxLength: number) => {
    if (text.length > maxLength) {
        return text.substring(0, maxLength) + '...';
    }
    return text;
};

const Template = ({template, handleDeleteTemplate} : TemplateProps) => {
    const [isDeleteDialogShown, setIsDeleteDialogShown] = useState<boolean>(false);
    const truncatedTitle = truncateText(template.title, 30);
    const truncatedDescription = truncateText(template.description, 60);
    const navigate = useNavigate();

    const handleDelete = async () => {
        handleDeleteTemplate(template.id);
        setIsDeleteDialogShown(false);
    }
    
    return (
        <Pane
            display="flex"
            flexDirection="column"
            border="default"
            borderRadius={8}
            padding={16}
            margin={8}
            width={280}
            elevation={2}
            hoverElevation={3}
            background="tint1">
            <Link
                onClick={() => navigate(routes.TEMPLATES.EDIT.replace(':id', template.id))}
                marginBottom={8}
                cursor="pointer">
                <Text
                    size={500}
                    fontWeight={500}
                    color="green600"
                    textOverflow="ellipsis">
                    {truncatedTitle}
                </Text>
            </Link>
            <Text
                fontSize={14}
                marginBottom={12}
                color="muted"
                textOverflow="ellipsis">
                {truncatedDescription}
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
                                <Menu.Item intent="access"
                                           onClick={() => window.open(routes.TEMPLATES.EDIT.replace(
                                               ':id', 
                                               template.id), 
                                               '_blank')}>
                                    Открыть в новой вкладке
                                </Menu.Item>
                                <Menu.Item intent="danger" 
                                           onClick={() => setIsDeleteDialogShown(true)}>
                                    Удалить
                                </Menu.Item>
                            </Menu.Group>
                        </Menu>
                    }>
                    <Icon
                        icon={CogIcon}
                        size={16}
                        cursor="pointer"
                    />
                </Popover>
            </Pane>

            <Dialog
                isShown={isDeleteDialogShown}
                title="Удаление шаблона"
                intent="danger"
                onCloseComplete={() => setIsDeleteDialogShown(false)}
                onConfirm={handleDelete}
                confirmLabel="Удалить"
            >
                Вы уверены, что хотите удалить шаблон?
            </Dialog>
        </Pane>
    );
};

export default Template;