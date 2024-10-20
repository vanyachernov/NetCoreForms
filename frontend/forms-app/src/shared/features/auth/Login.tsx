import {Button, Heading, Pane, TextInputField} from "evergreen-ui";
import {useEffect, useState} from "react";
import {Authenticate, AuthenticateUserRequest, isAuthenticated} from "../../apis/authService.ts";
import routes from "../../constants/routes.ts";
import {useNavigate} from "react-router-dom";

const Login = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();

    useEffect(() => {
        if (isAuthenticated()) {
            navigate(routes.TEMPLATES.ROOT);
        }
    }, []);
    
    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        
        const formData: AuthenticateUserRequest = { email, password };

        try {
            const response = await Authenticate(formData);

            if (response.data.result) {
                navigate(routes.TEMPLATES.ROOT);
            }
        } catch (error: any) {
            console.error("Ошибка авторизации:", error);
        }
    };
    
    return (
        <Pane 
            display="flex" 
            alignItems="center"
            justifyContent="center"
            paddingTop={60}>
            <Pane
                display="flex"
                flexDirection="column"
                width={300}
                padding={24}
                borderRadius={8}>
                <Heading 
                    size={600} 
                    marginBottom={16}
                    textAlign="center">
                    Авторизация
                </Heading>
                <Pane
                    background="gray200"
                    padding={20}
                    borderRadius={10}>
                    <form onSubmit={handleSubmit}>
                        <TextInputField
                            label="Логин"
                            description="Введите ваш адрес электронной почты"
                            placeholder="example@gmail.com"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                        />
                        <TextInputField
                            label="Пароль"
                            type="password"
                            placeholder="Введите ваш пароль"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                        />
                        <Button
                            appearance="primary"
                            marginTop={5}
                            onClick={handleSubmit}>
                            Войти
                        </Button>
                    </form>
                </Pane>
            </Pane>
        </Pane>
);
};

export default Login;