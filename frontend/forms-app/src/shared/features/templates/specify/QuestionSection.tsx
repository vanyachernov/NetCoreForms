import { Pane, TextInputField } from "evergreen-ui";
import React from "react";

interface QuestionSectionProps {
    id: string;
    title: string;
    description: string;
    onTitleChange: (value: string) => void;
    onDescriptionChange: (value: string) => void;
}

const QuestionSection = 
    ({ id, title, description, onTitleChange, onDescriptionChange } : QuestionSectionProps) => {
    return (
        <Pane
            id={id}
            background="white"
            borderRadius={10}
            overflow="hidden"
            marginBottom={25}
            borderWidth={1}
            borderColor="grey"
        >
            <Pane
                backgroundColor="green"
                width="100%"
                height={10}
                borderTopLeftRadius={10}
                borderTopRightRadius={10}
            />
            <Pane padding={20}>
                <TextInputField
                    label=""
                    placeholder="Введите название формы"
                    value={title}
                    onChange={(e: React.ChangeEvent<HTMLInputElement>) => onTitleChange(e.target.value)}
                    style={{
                        backgroundColor: "white",
                        border: "none",
                        fontSize: '40px',
                        lineHeight: '1.2',
                        height: '100px',
                    }}
                />
                <TextInputField
                    label=""
                    placeholder="Описание формы"
                    value={description}
                    onChange={(e: React.ChangeEvent<HTMLInputElement>) => onDescriptionChange(e.target.value)}
                    style={{
                        backgroundColor: "white",
                        border: "none",
                        fontSize: '20px',
                        lineHeight: '1',
                    }}
                />
            </Pane>
        </Pane>
    );
};

export default QuestionSection;
