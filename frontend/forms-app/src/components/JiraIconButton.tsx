import {IconButton, Tooltip, Text, Position, EnvelopeIcon, Popover, Pane, TextInput, Button} from 'evergreen-ui';
import React, {useState} from "react";
import {CreateAccount, JiraRequest} from "../shared/apis/jiraApi.ts";
import {GetUserFromToken} from "../shared/apis/authApi.ts";

function HelpButton() {
    const [title, setTitle] = useState<string>("");
    const [description, setDescription] = useState<string>("");

    const handleSubmit = async () => {
        const userData = GetUserFromToken();

        const userModel: JiraRequest = {
            summary: title ?? "",
            description: description ?? "",
            email: userData?.userEmail ?? "",
            currentUrl: window.location.href
        };

        await CreateAccount(userModel);
        
        close();
    };

    return (
        <Tooltip content="Нужна помощь?" position={Position.TOP}>
            <Popover
                content={({ close }) => (
                    <Pane
                        width={240}
                        height={240}
                        display="flex"
                        alignItems="center"
                        justifyContent="center"
                        flexDirection="column">
                        <Pane padding={16}>
                            
                            <Text fontSize={16}>
                                Создание задачи Jira
                            </Text>

                            <TextInput
                                placeholder="Название"
                                width="100%"
                                marginTop={14}
                                marginBottom={8}
                                value={title}
                                onChange={(e: React.ChangeEvent<HTMLInputElement>) => setTitle(e.target.value)}
                            />

                            <TextInput
                                placeholder="Описание"
                                width="100%"
                                marginBottom={16}
                                value={description}
                                onChange={(e: React.ChangeEvent<HTMLInputElement>) => setDescription(e.target.value)}
                            />

                            <Pane display="flex" justifyContent="space-between" width="100%">
                                <Button appearance="primary" intent="success" onClick={handleSubmit}>
                                    Отправить
                                </Button>
                                <Button appearance="minimal" intent="danger" onClick={() => {
                                    close();
                                    setTitle("");
                                    setDescription("");
                                }}>
                                    Отменить
                                </Button>
                            </Pane>
                        </Pane>
                    </Pane>
                )}
                position={Position.TOP_RIGHT}
            >
                <IconButton
                    icon={EnvelopeIcon}
                    color='white'
                    appearance="primary"
                    intent="none"
                    height={50}
                    width={50}
                    borderRadius="50%"
                    style={{
                        position: 'fixed',
                        bottom: 20,
                        right: 20,
                        backgroundColor: '#007bff'
                    }}
                />
            </Popover>
        </Tooltip>
    );
}

export default HelpButton;
