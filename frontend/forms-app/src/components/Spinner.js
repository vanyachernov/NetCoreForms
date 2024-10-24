import { jsx as _jsx } from "react/jsx-runtime";
import { Spinner as EvergreenSpinner, Pane } from "evergreen-ui";
const Spinner = () => {
    return (_jsx(Pane, { display: "flex", alignItems: "center", justifyContent: "center", height: 400, zIndex: 1000, children: _jsx(EvergreenSpinner, {}) }));
};
export default Spinner;
