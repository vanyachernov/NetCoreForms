import {Button, Heading, Link, Pane, Text, TextInputField} from "evergreen-ui";
import React, {useEffect, useState} from "react";
import {
    isAuthenticated,
    Register,
    RegisterUserRequest
} from "../../apis/authService.ts";
import routes from "../../constants/routes.ts";
import {useNavigate} from "react-router-dom";

const Login = () => {
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
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
                lastName,
                firstName
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
                navigate(routes.AUTH.SIGN_IN, { state: { showSuccessToast: true } });
            }
        } catch (error) {
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
                width={350}
                padding={24}
                borderRadius={8}>
                <Pane
                    background="gray200"
                    padding={20}
                    borderRadius={10}>
                    <Heading
                        size={600}
                        textAlign="left"
                        marginBottom={6}>
                        Регистрация
                    </Heading>
                    <Text>
                        Введите все необходимые данные, чтобы иметь доступ к системе.
                    </Text>
                    <form onSubmit={handleSubmit}>
                        <TextInputField
                            label="Имя"
                            type="text"
                            placeholder="Пример: Иван"
                            required
                            value={firstName}
                            marginTop={20}
                            onChange={(e: React.ChangeEvent<HTMLInputElement>) => setFirstName(e.target.value)}
                        />
                        <TextInputField
                            label="Фамилия"
                            type="text"
                            placeholder="Пример: Петров"
                            required
                            value={lastName}
                            onChange={(e: React.ChangeEvent<HTMLInputElement>) => setLastName(e.target.value)}
                        />
                        <TextInputField
                            label="Логин"
                            type="email"
                            placeholder="example@gmail.com"
                            required
                            value={email}
                            onChange={(e: React.ChangeEvent<HTMLInputElement>) => setEmail(e.target.value)}
                        />
                        <TextInputField
                            label="Пароль"
                            type="password"
                            placeholder="Пароль"
                            required
                            value={password}
                            onChange={(e: React.ChangeEvent<HTMLInputElement>) => setPassword(e.target.value)}
                        />
                        <Text
                            display="flex"
                            justifyContent="center"
                            alignItems="center"
                            marginBottom={10}
                            gap={6}>
                            У Вас уже есть аккаунт?
                            <Link
                                href={routes.AUTH.SIGN_IN}>
                                Войдите!
                            </Link>
                        </Text>
                        <Button
                            appearance="primary"
                            intent="success"
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