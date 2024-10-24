import { jsx as _jsx, jsxs as _jsxs } from "react/jsx-runtime";
import { useEffect, useState } from 'react';
import { Image, Pane } from "evergreen-ui";
import Greetings from "../../../components/Greetings.tsx";
const Home = () => {
    const [isMobile, setIsMobile] = useState(window.innerWidth < 768);
    useEffect(() => {
        const handleResize = () => {
            setIsMobile(window.innerWidth < 768);
        };
        window.addEventListener('resize', handleResize);
        return () => window.removeEventListener('resize', handleResize);
    }, []);
    return (_jsx(Pane, { children: _jsxs(Pane, { height: "95vh", display: "flex", flexDirection: isMobile ? 'column' : 'row', paddingLeft: 96, paddingRight: 96, paddingTop: 25, alignItems: "center", justifyContent: "space-between", children: [_jsx(Pane, { flex: isMobile ? '0 0 100%' : '0 0 33.33%', alignItems: isMobile ? 'center' : 'flex-start', padding: 16, children: _jsx(Greetings, {}) }), _jsx(Pane, { flex: isMobile ? '0 0 100%' : '0 0 66.67%', display: "flex", justifyContent: "center", padding: 16, children: _jsx(Pane, { flex: 1, display: "flex", justifyContent: "center", alignItems: "center", children: _jsx(Image, { src: "./src/assets/images/greetings_bg.png", alt: "\u041F\u0440\u0438\u043C\u0435\u0440 \u0448\u0430\u0431\u043B\u043E\u043D\u0430", width: "100%", maxWidth: isMobile ? 400 : 800, height: "auto" }) }) })] }) }));
};
export default Home;
