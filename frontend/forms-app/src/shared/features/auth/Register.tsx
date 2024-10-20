import {Button, Heading, Pane, TextInputField} from "evergreen-ui";
import {useEffect, useState} from "react";
import {
    isAuthenticated,
    Register,
    RegisterUserRequest
} from "../../apis/authService.ts";
import routes from "../../constants/routes.ts";
import {useNavigate} from "react-router-dom";

const Login = () => {
    const [name, setName] = useState("");
    const [surname, setSurname] = useState("");
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

        const formData: RegisterUserRequest = { 
            fullName: {
                surname,
                name
            },
            email: {
                email
            },
            password: {
                password
            }
        };

        try {
            const response = await Register(formData);

            if (response) {
                navigate(routes.LOGIN);
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
            paddingTop={10}>
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
                    Регистрация
                </Heading>
                <Pane
                    background="gray200"
                    padding={20}
                    borderRadius={10}>
                    <form onSubmit={handleSubmit}>
                        <TextInputField
                            label="Имя"
                            description="Введите Ваше имя"
                            placeholder="Пример: Иван"
                            value={email}
                            onChange={(e) => setName(e.target.value)}
                        />
                        <TextInputField
                            label="Фамилия"
                            description="Введите Вашу фамилию"
                            placeholder="Пример: Петров"
                            value={email}
                            onChange={(e) => setSurname(e.target.value)}
                        />
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
                            description="Придумайте свой пароль"
                            placeholder="Пароль"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                        />
                        <Button
                            appearance="primary"
                            marginTop={5}
                            onClick={handleSubmit}
                            width="100%">   
                            Зарегистрироваться
                        </Button>
                    </form>
                </Pane>
            </Pane>
        </Pane>
    );
};

export default Login;