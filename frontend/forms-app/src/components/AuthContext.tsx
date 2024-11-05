import { createContext, useContext, useState, ReactNode } from 'react';
import Cookies from 'js-cookie';

interface AuthContextType {
    isAuth: boolean;
    updateAuthStatus: () => void;
}

const AuthContext = createContext<AuthContextType | null>(null);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
    const [isAuth, setIsAuth] = useState<boolean>(!!Cookies.get("accessToken"));

    const updateAuthStatus = () => {
        setIsAuth(!!Cookies.get("accessToken"));
    };

    return (
        <AuthContext.Provider value={{ isAuth, updateAuthStatus }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error("useAuth must be used within an AuthProvider");
    }
    return context;
};
