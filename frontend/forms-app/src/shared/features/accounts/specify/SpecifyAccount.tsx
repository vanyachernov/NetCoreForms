import {useEffect, useState} from "react";
import {Pane, Text, Heading} from "evergreen-ui";
import {GetUserFromToken, isAuthenticated} from "../../../apis/authApi.ts";
import {CreateAccount, SalesforceRequest} from "../../../apis/salesforceApi.ts";
import routes from "../../../constants/routes.ts";
import {useNavigate} from "react-router-dom";
import {getTasks, TaskViewModel} from "../../../apis/jiraApi.ts";
import TaskCard from "./shared/TaskCard.tsx";

const SpecifyAccount = () => {
    const [tasks, setTasks] = useState<TaskViewModel[]>([]);
    const [selectedTab, setSelectedTab] = useState<string>("account");
    const navigate = useNavigate();
    const userData = GetUserFromToken();

    const fetchTasks = async () => {
        if (userData?.userEmail) {
            const tasks = await getTasks(userData.userEmail);

            console.log(tasks);
            setTasks(tasks);
        }
    };
    
    useEffect(() => {
        navigate(isAuthenticated()
            ? routes.ACCOUNT.ROOT
            : routes.AUTH.SIGN_IN);
    }, [navigate]);

    useEffect(() => {
        fetchTasks();
    }, []);
    
    const handleSalesforce = async () => {

        const userModel: SalesforceRequest = {
            AccountName: userData?.userEmail.split('@')[0] ?? "",
            FirstName: userData?.userFirstname ?? "",
            LastName: userData?.userLastname ?? "",
            Email: userData?.userEmail ?? ""
        };
        
        await CreateAccount(userModel);

        if (userData?.userEmail) {
            const updatedTasks = await getTasks(userData.userEmail);
            setTasks(updatedTasks);
        }
    }
    
    return (
        <Pane display="flex" 
              height="100vh">
            <Pane width={240} 
                  background="#DFF2EB"
                  display="flex" 
                  flexDirection="column">
                <Heading size={600}
                         padding={16}
                         marginBottom={16}>
                    Меню
                </Heading>
                <Pane display="flex" flexDirection="column" alignItems="center" justifyContent="center" width="100%" gap={15}>
                    <Pane
                        width="90%"
                        borderRadius={3}
                        padding={6}
                        backgroundColor={selectedTab === "account" ? "#A7DADB" : "#DFF2EB"}
                        cursor="pointer"
                        onClick={() => setSelectedTab("account")}>
                        <Text size={500} fontWeight={selectedTab === "account" ? 600 : 400} color={selectedTab === "account" ? "#2C4A52" : "#4A7C7A"}>
                            Информация об аккаунте
                        </Text>
                    </Pane>

                    <Pane
                        width="90%"
                        borderRadius={3}
                        padding={6}
                        backgroundColor={selectedTab === "jira" ? "#A7DADB" : "#DFF2EB"}
                        cursor="pointer"
                        onClick={() => setSelectedTab("jira")}>
                        <Text size={500} fontWeight={selectedTab === "jira" ? 600 : 400} color={selectedTab === "jira" ? "#2C4A52" : "#4A7C7A"}>
                            Задания Jira
                        </Text>
                    </Pane>

                    <Pane
                        width="90%"
                        padding={6}
                        backgroundColor={selectedTab === "salesforce" ? "#A7DADB" : "#DFF2EB"}
                        cursor="pointer"
                        onClick={handleSalesforce}>
                        <Text size={500} fontWeight={selectedTab === "salesforce" ? 600 : 400} color={selectedTab === "salesforce" ? "#2C4A52" : "#4A7C7A"}>
                            Salesforce
                        </Text>
                    </Pane>
                </Pane>
            </Pane>

            <Pane flex="1" padding={16}>
                {selectedTab === "account" && (
                    <Pane textAlign="center" marginTop={20}>
                        <Text size={600} fontWeight={500} marginBottom={16}>
                            Информация о аккаунте
                        </Text>
                        <Pane display="flex" flexDirection="column" textAlign="left" alignItems="flex-start" padding={20} gap={10}>
                            <Pane display="flex">
                                <Text size={500} fontWeight={600} width={120}>
                                    Имя:
                                </Text>
                                <Text size={500}>{userData?.userFirstname ?? "Не указано"}</Text>
                            </Pane>
                            <Pane display="flex">
                                <Text size={500} fontWeight={600} width={120}>
                                    Фамилия:
                                </Text>
                                <Text size={500}>{userData?.userLastname ?? "Не указано"}</Text>
                            </Pane>
                            <Pane display="flex">
                                <Text size={500} fontWeight={600} width={120}>
                                    Роль:
                                </Text>
                                <Text size={500}>{userData?.userRole ?? "Не указано"}</Text>
                            </Pane>
                            <Pane display="flex">
                                <Text size={500} fontWeight={600} width={120}>
                                    Email:
                                </Text>
                                <Text size={500}>{userData?.userEmail ?? "Не указано"}</Text>
                            </Pane>
                        </Pane>
                    </Pane>
                )}

                {selectedTab === "jira" && (
                    <Pane textAlign="center" marginTop={20}>
                        <Text size={600} fontWeight={500} marginBottom={16}>
                            Информация о задачах
                        </Text>
                        <Pane display="flex" alignItems="center" flexDirection="column" padding={20} gap={10}>
                            {tasks.map((task) => (
                                <TaskCard key={task.id} task={task} />
                            ))}
                        </Pane>
                    </Pane>
                )}
            </Pane>
        </Pane>
    );
};

export default SpecifyAccount;
