import {useEffect, useState} from "react";
import {getTemplates, getTemplatesById, TemplateViewModel} from "../../../apis/templateApi.ts";
import {EmptyState, Heading, Pane, SearchIcon} from "evergreen-ui";
import Template from "../../../../components/Template.tsx";
import Spinner from "../../../../components/Spinner.tsx";
import urls from "../../../constants/urls.ts";
import {GetUserFromToken} from "../../../apis/authService.ts";
import {roles} from "../../../logic/roles.ts";

const TemplateList = () => {
    const [templates, setTemplates] = useState<TemplateViewModel[]>([])
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const user = GetUserFromToken();
    
    useEffect(() => {
        const fetchData = async () => {
            try {
                let responseData = null;

                if (user?.userRole === roles.ADMIN) {
                    responseData = await getTemplates();
                } else {
                    responseData = await getTemplatesById(user!.userId.toString());
                }
                
                setTemplates(responseData);
            } catch (error) {
                console.error("Error fetching templates:", error);
            } finally {
                setIsLoading(false);
            }
        };
        
        fetchData();
    }, [user]);

    if (isLoading) {
        return <Spinner />;
    }
    
    return (
        <Pane>
            <Heading
                size={500}
                marginBottom={16}
                marginLeft={10}
                marginTop={6}>
                Недавние шаблоны
            </Heading>
            {templates.length > 0 ? (
                <Pane
                    display="flex"
                    alignItems="center"
                    justifyContent="center">
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
                <EmptyState
                    background="light"
                    title="No templates yet!"
                    orientation="horizontal"
                    icon={<SearchIcon color="#C1C4D6" />}
                    iconBgColor="#EDEFF5"
                    description="Unfortunately, no templates have been created yet."
                    anchorCta={
                        <EmptyState.LinkButton href={urls.USERS.CREATE_USER_TEMPLATE} target="_blank">
                            You can create your first template if you want to
                        </EmptyState.LinkButton>
                    }
                />
            )}
        </Pane>
    );
};

export default TemplateList;