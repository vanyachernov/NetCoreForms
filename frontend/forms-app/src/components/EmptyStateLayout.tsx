import React from 'react';
import {EmptyState, SearchIcon} from "evergreen-ui";
import urls from "../shared/constants/urls.ts";

export interface EmptyStateLayoutProps {
    title: string;
    description: string;
    tip: string;
}

const EmptyStateLayout = ({title, description, tip} : EmptyStateLayoutProps) => {
    return (
        <EmptyState
            background="light"
            title={title}
            orientation="horizontal"
            icon={<SearchIcon color="#C1C4D6" />}
            iconBgColor="#EDEFF5"
            description={description}
            anchorCta={
                <EmptyState.LinkButton href={urls.USERS.CREATE_USER_TEMPLATE} target="_blank">
                    {tip}
                </EmptyState.LinkButton>
            }
        />
    );
};

export default EmptyStateLayout;