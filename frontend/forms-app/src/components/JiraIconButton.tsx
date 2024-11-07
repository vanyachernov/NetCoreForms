import {
    IconButton,
    Tooltip,
    Position,
    Dialog,
    Pane,
    TextInput, 
    Label
} from 'evergreen-ui';
import { MdQuestionMark } from "react-icons/md";
import React, { useState } from "react";
import { CreateAccount, JiraRequest } from "../shared/apis/jiraApi.ts";
import { GetUserFromToken } from "../shared/apis/authApi.ts";
import Select, {StylesConfig} from "react-select";

function HelpButton() {
    const [isDialogOpen, setIsDialogOpen] = useState(false);
    const [title, setTitle] = useState<string>("");
    const [description, setDescription] = useState<string>("");
    const [priority, setPriority] = useState<string>("Medium");
    
    const priorities = [
        "Low",
        "Medium",
        "High"
    ];

    const priorityOptions = priorities.map((label) => ({ label, value: label }));

    type PriorityOption = {
        label: string;
        value: string;
    };
    
    const priorityCustomStyles: StylesConfig<PriorityOption> = {
        control: (provided) => ({
            ...provided,
            fontSize: '12px',
        }),
        option: (provided) => ({
            ...provided,
            fontSize: '12px',
        }),
        singleValue: (provided) => ({
            ...provided,
            fontSize: '12px',
        }),
        menuPortal: (provided) => ({
            ...provided,
            zIndex: 9999,
        })
    };
    
    const handleSubmit = async () => {
        const userData = GetUserFromToken();

        const userModel: JiraRequest = {
            Summary: title,
            Description: description,
            Email: userData?.userEmail ?? "",
            Priority: priority,
            Link: window.location.href,
            TemplateName: ""
        };

        await CreateAccount(userModel);

        setIsDialogOpen(false);
        
        setTitle("");
        setDescription("");
        setPriority("Medium");
    };

    return (
        <>
            <Tooltip content="Нужна помощь?" position={Position.TOP}>
                <IconButton
                    icon={MdQuestionMark}
                    color='white'
                    appearance="primary"
                    intent="none"
                    height={50}
                    width={50}
                    borderRadius="50%"
                    onClick={() => setIsDialogOpen(true)}
                    style={{
                        position: 'fixed',
                        bottom: 20,
                        right: 20,
                        backgroundColor: '#007bff'
                    }}
                />
            </Tooltip>

            <Dialog
                isShown={isDialogOpen}
                title="Создание задачи Jira"
                onCloseComplete={() => setIsDialogOpen(false)}
                confirmLabel="Отправить"
                cancelLabel="Отменить"
                onConfirm={handleSubmit}>
                <Pane paddingX={16} 
                      paddingY={8}>
                    <Label htmlFor="title" 
                           marginBottom={10} 
                           display="block">
                        Название задачи
                    </Label>
                    <TextInput
                        id="title"
                        placeholder="Введите название задачи"
                        width="100%"
                        marginBottom={16}
                        value={title}
                        onChange={(e: React.ChangeEvent<HTMLInputElement>) => setTitle(e.target.value)}
                    />

                    <Label htmlFor="description" 
                           marginBottom={10} 
                           display="block">
                        Описание задачи
                    </Label>
                    <TextInput
                        id="description"
                        placeholder="Введите описание задачи"
                        width="100%"
                        marginBottom={16}
                        value={description}
                        onChange={(e: React.ChangeEvent<HTMLInputElement>) => setDescription(e.target.value)}
                    />

                    <Label htmlFor="priority" 
                           marginBottom={10} 
                           display="block">
                        Приоритет
                    </Label>
                    <Select
                        options={priorityOptions}
                        value={priorityOptions.find(option => option.value === priority)}
                        onChange={(selectedOption) => setPriority(selectedOption?.value || '')}
                        placeholder="Выберите приоритет..."
                        styles={priorityCustomStyles}
                        menuPortalTarget={document.body}
                        isMulti={false}
                    />

                    <Label htmlFor="link"
                           marginTop={16}
                           marginBottom={10}
                           display="block">
                        Текущая ссылка
                    </Label>
                    <TextInput 
                        id="link"
                        width="100%"
                        value={window.location.href}
                        disabled />
                </Pane>
            </Dialog>
        </>
    );
}

export default HelpButton;
