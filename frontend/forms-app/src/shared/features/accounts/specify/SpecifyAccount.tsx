import {useEffect, useState} from "react";
import { Pane, Text, Tablist, Tab, Heading} from "evergreen-ui";
import {GetUserFromToken, isAuthenticated} from "../../../apis/authApi.ts";
import {CreateAccount, SalesforceRequest} from "../../../apis/salesforceApi.ts";
import routes from "../../../constants/routes.ts";
import {useNavigate} from "react-router-dom";

const SpecifyAccount = () => {
    const [selectedTab, setSelectedTab] = useState<string>("account");
    const navigate = useNavigate();
    
    useEffect(() => {
        navigate(isAuthenticated()
            ? routes.ACCOUNT.ROOT
            : routes.AUTH.SIGN_IN);
    }, [navigate]);
    
    const handleSalesforce = async () => {
        const userData = GetUserFromToken();

        const userModel: SalesforceRequest = {
            AccountName: userData?.userEmail.split('@')[0] ?? "",
            FirstName: userData?.userFirstname ?? "",
            LastName: userData?.userLastname ?? "",
            Email: userData?.userEmail ?? ""
        };
        
        await CreateAccount(userModel);
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
                <Tablist marginBottom={16} 
                         display="flex"
                         flexDirection="column" 
                         alignItems="center"
                         justifyContent="center"
                         width="100%"
                         gap={15}>
                    <Tab id="account" 
                         key="account" 
                         isSelected={selectedTab === "account"} 
                         onSelect={() => setSelectedTab("account")}>
                        Информация об аккаунте
                    </Tab>
                    <Tab id="salesforce"
                         key="salesforce"
                         onSelect={handleSalesforce}>
                        Salesforce
                    </Tab>
                </Tablist>
            </Pane>

            <Pane flex="1" padding={16}>
                {selectedTab === "account" && (
                    <Pane>
                        <Text size={500}>Информация о аккаунте (тестовый текст)</Text>
                    </Pane>
                )}
            </Pane>
            
        </Pane>
    );
};

export default SpecifyAccount;
