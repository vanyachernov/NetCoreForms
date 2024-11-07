import { Pane, Text, Badge, Link } from "evergreen-ui";
import {JIRA_BASE_URL, TaskViewModel} from "../../../../apis/jiraApi.ts";

interface TaskCardProps {
    task: TaskViewModel;
}

const TaskCard = ({ task }: TaskCardProps) => {
    return (
        <Pane
            border
            elevation={1}
            padding={16}
            marginY={12}
            background="tint1"
            display="flex"
            flexDirection="column"
            position="relative"
            width="100%"
            maxWidth={600}>
            
            <Badge color="blue" position="absolute" top={8} right={8}>
                {task.fields.type}
            </Badge>

            <Pane display="flex" alignItems="center" marginTop="auto">
                <Text size={400} fontWeight={500} marginRight={8}>
                    Статус задачи:
                </Text>
                <Badge color={task.fields.status === "К выполнению" 
                    ? "red" 
                    : task.fields.status === "Готово" 
                        ? "green" 
                        : "neutral"}>
                    {task.fields.status}
                </Badge>
            </Pane>
            
            <Pane marginBottom={6}>
                <Link
                    href={`${JIRA_BASE_URL}/${task.key}`}
                    target="_blank"
                    size={600}
                    color="blue"
                    fontWeight={600}>
                    {task.fields.summary}
                </Link>
            </Pane>
            
            <Text size={400} color="muted" marginBottom={12}>
                {task.fields.description || "Нет описания"}
            </Text>

            <Pane display="flex" alignItems="center" marginTop="auto">
                <Text size={400} fontWeight={500} marginRight={8}>
                    Приоритет:
                </Text>
                <Badge color="red">{task.fields.priority.value}</Badge>
            </Pane>

            <Pane position="absolute" bottom={16} right={10} display="flex" alignItems="center" marginRight="auto">
                <Text size={400} fontWeight={500} marginRight={8}>
                    Код: {task.key}
                </Text>
            </Pane>
        </Pane>
    );
};

export default TaskCard;
