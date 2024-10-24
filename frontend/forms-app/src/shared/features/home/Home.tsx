import {useEffect, useState} from 'react';
import {Image, Pane} from "evergreen-ui";
import Greetings from "../../../components/Greetings.tsx";

const Home = () => {
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
            <Pane
                height="95vh"
                display="flex"
                flexDirection={isMobile ? 'column' : 'row'}
                paddingLeft={96}
                paddingRight={96}
                paddingTop={25}
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
                            src="../../public/assets/images/greetings_bg.png"
                            alt="Пример шаблона"
                            width="100%"
                            maxWidth={isMobile ? 400 : 800}
                            height="auto"
                        />
                    </Pane>
                </Pane>
            </Pane>
        </Pane>
    );
};

export default Home;