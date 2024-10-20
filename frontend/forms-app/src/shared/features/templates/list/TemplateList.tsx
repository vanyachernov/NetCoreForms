import {useEffect, useState} from "react";
import {getTemplates, TemplateViewModel} from "../../../apis/templateApi.ts";
import {Heading, Pane} from "evergreen-ui";
import Template from "../../../../components/Template.tsx";
import {Await} from "react-router-dom";

const TemplateList = () => {
    const [templates, setTemplates] = useState<TemplateViewModel[]>([])

    useEffect(() => {
        const fetchData = async() => {
            const responseData = await getTemplates();
            console.log(responseData);
            setTemplates(responseData);
        };
        
        fetchData();
    }, []);
    
    return (
        <Pane>
            <Heading>
                {templates.length > 0 ? (
                    templates.map((template, index) => (
                        <Template key={index} template={template} />
                    ))
                ) : (
                    <div>No templates available</div>
                )}
            </Heading>
        </Pane>
    );
};

export default TemplateList;