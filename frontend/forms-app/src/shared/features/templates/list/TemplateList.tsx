import {useEffect, useState} from "react";
import {createTemplate, getTemplates, getTemplatesById, TemplateViewModel} from "../../../apis/templateApi.ts";
import {Heading, Pane, Text, toaster} from "evergreen-ui";
import Template from "../../../../components/Template.tsx";
import Spinner from "../../../../components/Spinner.tsx";
import {GetUserFromToken, isAuthenticated} from "../../../apis/authService.ts";
import {roles} from "../../../logic/roles.ts";
import EmptyStateLayout from "../../../../components/EmptyStateLayout.tsx";
import {useNavigate} from "react-router-dom";
import routes from "../../../constants/routes.ts";

const TemplateList = () => {
    const [templates, setTemplates] = useState<TemplateViewModel[]>([])
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const user = GetUserFromToken();
    const navigate = useNavigate();
    
    useEffect(() => {
        const fetchData = async () => {
            try {
                if (user === null) {
                    navigate(routes.AUTH.SIGN_IN);
                    return;
                }

                let responseData = null;

                if (user.userRole === roles.ADMIN) {
                    responseData = await getTemplates();
                } else {
                    responseData = await getTemplatesById(user.userId.toString());
                }

                setTemplates(responseData);
            } catch (error) {
                console.error("Error fetching templates:", error);
            } finally {
                setIsLoading(false);
            }
        };
        
        fetchData();
    }, []);

    const handleCreateEmptyTemplate = async () => {
        if (!isAuthenticated()) {
            navigate(routes.AUTH.SIGN_IN);
            return;
        }
        
        if (!user?.userId)
        {
            console.error("User identifier is undefined!");
            return;
        }

        try {
            const templateData = {
                title: "Мой новый шаблон",
                description: "Описание для нового шаблона"
            }
            
            const createdTemplateIdentifier = await createTemplate(
                user?.userId, 
                templateData);
            
            if (createdTemplateIdentifier) {
                const editPath = routes.TEMPLATES.EDIT.replace(':id', createdTemplateIdentifier);
                navigate(editPath);
            } else {
                toaster.danger('Создание шаблона', {
                    description: 'Во время создания шаблона возникла ошибка.',
                    duration: 5,
                })
            }
        } catch (error) {
            console.error("Error creating template:", error);
        }
    };

    if (isLoading) {
        return <Spinner />;
    }
    
    return (
        <Pane>
            <Pane
                display="flex"
                backgroundColor="#C1D7C9"
                flexDirection="column"
                padding={20}>
                <Pane>
                    <Heading
                        size={500}
                        fontWeight={500}
                        marginBottom={16}
                        marginLeft={10}
                        marginTop={6}>
                        Создать форму
                    </Heading>

                    <Pane
                        display="flex"
                        flexDirection="column"
                        alignItems="left"
                        marginLeft={10}>

                        <Pane
                            display="flex"
                            flexDirection="column"
                            alignItems="center"
                            justifyContent="center"
                            width={160}
                            height={120}
                            border="solid"
                            borderWidth={0.1}
                            borderColor="green"
                            borderRadius={8}
                            cursor="pointer"
                            onClick={handleCreateEmptyTemplate}
                            backgroundColor="white">
                            <Pane
                                width={50}
                                height={50}
                                backgroundImage="url('https://cdn-icons-png.flaticon.com/512/2997/2997933.png')"
                                backgroundSize="contain"
                                backgroundPosition="center"
                                backgroundRepeat="no-repeat"
                            />
                        </Pane>
                        <Text
                            size={400}
                            fontWeight={500}
                            marginTop={6}
                            marginLeft={6}>
                            Пустая форма
                        </Text>
                    </Pane>
                </Pane>
            </Pane>
            
            <Pane 
                display="flex"
                flexDirection="column"
                padding={10}>
                
                <Pane 
                    marginLeft={10}
                    marginTop={10}>
                    
                    <Heading
                        size={500}
                        fontWeight={500}
                        marginBottom={16}
                        marginLeft={10}
                        marginTop={6}>
                        Недавние шаблоны
                    </Heading>

                    {templates.length > 0 ? (
                        <Pane
                            display="flex"
                            alignItems="left"
                            justifyContent="left">
                            <Pane
                                display="flex"
                                flexDirection="row"
                                flexWrap="wrap"
                                alignItems="center">
                                {templates.map((template, index) => (
                                    <Template key={index} template={template} />
                                ))}
                            </Pane>
                        </Pane>
                    ) : (
                        <Pane 
                            display="flex"
                            justifyContent="center"
                            alignItems="center"
                            height="100vh">
                            <EmptyStateLayout
                                title="Форм нет!"
                                description="Чтобы начать, выберите пустую форму или один из недавних шаблонов."
                                tip="" />
                        </Pane>
                    )}
                </Pane>
            </Pane>
        </Pane>
    );
};

export default TemplateList;