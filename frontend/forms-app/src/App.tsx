import './App.css'
import { Pane, Image } from 'evergreen-ui';
import NavigationPanel from "./components/NavigationPanel.tsx";
import Greetings from "./components/Greetings.tsx";
import {useEffect, useState} from "react";

function App() {
    const [isMobile, setIsMobile] = 
        useState(window.innerWidth < 768);

    useEffect(() => {
        const handleResize = () => {
            setIsMobile(window.innerWidth < 768);
        };
        
        window.addEventListener('resize', handleResize);
        return () => window.removeEventListener('resize', handleResize);
    }, []);
    
    return (
        <Pane>
            <NavigationPanel />
            <Pane
                display="flex"
                flexDirection={isMobile ? 'column' : 'row'}
                padding={48}
                alignItems="center"
                justifyContent="space-between"
            >
                <Pane 
                    flex={isMobile ? '0 0 100%' : '0 0 33.33%'}
                    alignItems={isMobile ? 'center' : 'flex-start'}
                    padding={16}>
                    <Greetings />
                </Pane>

                <Pane
                    flex={isMobile ? '0 0 100%' : '0 0 66.67%'}
                    display="flex"
                    justifyContent="center"
                    padding={16}
                >
                    <Pane flex={1} display="flex" justifyContent="center" alignItems="center">
                        <Image
                            src="./src/assets/images/greetings_bg.png"
                            alt="Пример шаблона"
                            width="100%"
                            maxWidth={isMobile ? 400 : 800}
                            height="auto"
                        />
                    </Pane>
                </Pane>
            </Pane>      
        </Pane>
      )
}

export default App
