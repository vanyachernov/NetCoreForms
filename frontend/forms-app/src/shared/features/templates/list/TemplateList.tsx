import {useEffect, useState} from "react";
import {getTemplates, TemplateViewModel} from "../../../apis/templateApi.ts";

const TemplateList = () => {
    const [templates, setTemplates] = useState<TemplateViewModel[]>()

    useEffect(() => {
        (async () => {
            const responseData = await getTemplates();
            setTemplates(responseData);
        })();
    }, []);
    
    return (
        <div>
            
        </div>
    );
};

export default TemplateList;