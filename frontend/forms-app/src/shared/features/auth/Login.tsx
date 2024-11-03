import {Button, Heading, Pane, TextInputField, Text, Link, toaster} from "evergreen-ui";
import React, {useEffect, useState} from "react";
import {Authenticate, AuthenticateUserRequest, isAuthenticated} from "../../apis/authApi.ts";
import routes from "../../constants/routes.ts";
import {useLocation, useNavigate} from "react-router-dom";

const Login = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();
    const location = useLocation();

    useEffect(() => {
        if (location.state?.showSuccessToast) {
            toaster.success('Аккаунт', {
                description: 'Ваш аккаунт успешно создан! Войдите в него.',
                duration: 3,
            });
        }
    }, [location.state]);
    
    useEffect(() => {
        if (isAuthenticated()) {
            navigate(routes.TEMPLATES.ROOT);
        }
    }, [navigate]);
    
    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        
        const formData: AuthenticateUserRequest = { email, password };

        try {
            const response = await Authenticate(formData);

            if (response && response.data && response.data.result) {
                navigate(routes.TEMPLATES.ROOT);
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
                padding={10}
                borderRadius={8}>
                <Pane
                    background="gray200"
                    padding={20}
                    borderRadius={10}>
                    <Heading
                        size={600}
                        textAlign="left"
                        marginBottom={6}>
                        Авторизация
                    </Heading>
                    <Text>
                        Введіть свій логін та пароль, щоб увійти у систему.
                    </Text>
                    <form onSubmit={handleSubmit}>
                        <TextInputField
                            label="Почта"
                            type="email"
                            placeholder="example@gmail.com"
                            value={email}
                            marginTop={20}
                            onChange={(e: React.ChangeEvent<HTMLInputElement>) => setEmail(e.target.value)}
                        />
                        <TextInputField
                            label="Пароль"
                            type="password"
                            placeholder="Введите ваш пароль"
                            value={password}
                            onChange={(e: React.ChangeEvent<HTMLInputElement>) => setPassword(e.target.value)}
                        />
                        <Text
                            display="flex"
                            justifyContent="center"
                            alignItems="center"
                            marginBottom={10}
                            gap={6}>
                            У Вас нет аккаунта? 
                            <Link
                                href={routes.AUTH.SIGN_UP}>
                                Создайте его!
                            </Link>
                        </Text>
                        <Button
                            appearance="primary"
                            intent="success"
                            marginTop={8}
                            onClick={handleSubmit}
                            width="100%">
                            Войти
                        </Button>
                    </form>
                </Pane>
            </Pane>
        </Pane>
);
};

export default Login;