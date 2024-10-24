import { Spinner as EvergreenSpinner, Pane } from "evergreen-ui";

const Spinner = () => {
    return (
        <Pane
            display="flex"
            alignItems="center"
            justifyContent="center"
            height={400}
            zIndex={1000}>
            <EvergreenSpinner />
        </Pane>
    );
};

export default Spinner;
