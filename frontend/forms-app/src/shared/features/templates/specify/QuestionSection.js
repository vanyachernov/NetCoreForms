import { jsx as _jsx, jsxs as _jsxs } from "react/jsx-runtime";
import { Pane, TextInputField } from "evergreen-ui";
const QuestionSection = ({ id, title, description, onTitleChange, onDescriptionChange }) => {
    return (_jsxs(Pane, { id: id, background: "white", borderRadius: 10, overflow: "hidden", marginBottom: 25, borderWidth: 1, borderColor: "grey", children: [_jsx(Pane, { backgroundColor: "green", width: "100%", height: 10, borderTopLeftRadius: 10, borderTopRightRadius: 10 }), _jsxs(Pane, { padding: 20, children: [_jsx(TextInputField, { label: "", placeholder: "\u0412\u0432\u0435\u0434\u0438\u0442\u0435 \u043D\u0430\u0437\u0432\u0430\u043D\u0438\u0435 \u0444\u043E\u0440\u043C\u044B", value: title, onChange: (e) => onTitleChange(e.target.value), style: {
                            backgroundColor: "white",
                            border: "none",
                            fontSize: '40px',
                            lineHeight: '1.2',
                            height: '100px',
                        } }), _jsx(TextInputField, { label: "", placeholder: "\u041E\u043F\u0438\u0441\u0430\u043D\u0438\u0435 \u0444\u043E\u0440\u043C\u044B", value: description, onChange: (e) => onDescriptionChange(e.target.value), style: {
                            backgroundColor: "white",
                            border: "none",
                            fontSize: '20px',
                            lineHeight: '1',
                        } })] })] }));
};
export default QuestionSection;
