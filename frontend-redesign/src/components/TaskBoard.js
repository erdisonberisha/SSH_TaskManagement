import React, { useState, useEffect } from 'react';
import { DragDropContext, Droppable, Draggable } from 'react-beautiful-dnd';
import { getTasks } from '../helpers/api';

// Enums
const StatusType = {
    TODO: 0,
    INPROGRESS: 1,
    TESTING: 2,
    COMPLETED: 3,
    OVERDUE: 4,
};

const PriorityType = {
    LOW: 1,
    MEDIUM: 2,
    HIGH: 3,
};

const TaskBoard = () => {
    const [taskList, setTaskList] = useState([]);

    useEffect(() => {
        const fetchTasks = async () => {
          try {
            const tasks = await getTasks();
            console.log('test')
            setTaskList(tasks);
          } catch (error) {
            console.log('Error fetching tasks!')
          }
        };
        fetchTasks();
      }, []);

    const onDragEnd = (result) => {
        const { destination, source } = result;

        // Check if dropped outside of a droppable area
        if (!destination) return;

        // Check if dropped in a different position
        if (
            destination.droppableId === source.droppableId &&
            destination.index === source.index
        ) {
            return;
        }

        const updatedTaskList = [...taskList];
        const [draggedTask] = updatedTaskList.splice(source.index, 1);
        draggedTask.status = parseInt(destination.droppableId.split('-')[1], 10);
        updatedTaskList.splice(destination.index, 0, draggedTask);

        setTaskList(updatedTaskList);
    };

    const getColumnWidth = () => {
        const columnCount = Object.values(StatusType).length;
        const containerWidth = 100; // Percentage of container width
        const gutterWidth = 2; // Percentage of gutter width
        const columnWidth = (containerWidth - (columnCount - 1) * gutterWidth) / columnCount;
        return columnWidth;
      };
    

    return (
        <div className="flex flex-grow">
            <div className="flex space-x-4 bg-gray-100 p-4 rounded w-full">
                <DragDropContext onDragEnd={onDragEnd}>
                    {Object.values(StatusType).map((status) => (
                        <Droppable key={status} droppableId={`status-${status}`} type="TASK">
                            {(provided) => (
                                <div
                                    className="flex flex-col min-w-0"
                                    style={{ width: `${getColumnWidth()}%` }}
                                >
                                    <div className="flex items-center mb-2">
                                        <span className="font-semibold">{getStatusLabel(status)}</span>
                                    </div>
                                    <div
                                        className="bg-white rounded shadow p-2 space-y-2"
                                        {...provided.droppableProps}
                                        ref={provided.innerRef}
                                    >
                                        {taskList.map((task, index) => {
                                            if (task.status === status) {
                                                return (
                                                    <Draggable
                                                        key={task.id}
                                                        draggableId={`task-${task.id}`}
                                                        index={index}
                                                    >
                                                        {(provided) => (
                                                            <div
                                                                className="bg-gray-200 p-2 rounded shadow"
                                                                {...provided.draggableProps}
                                                                {...provided.dragHandleProps}
                                                                ref={provided.innerRef}
                                                            >
                                                                <div className="flex items-center space-x-2">
                                                                    <div className="w-4 h-4 bg-blue-500 rounded shadow" />
                                                                    <span>{task.title}</span>
                                                                </div>
                                                                <div className="text-xs text-gray-500">
                                                                    {task.description}
                                                                </div>
                                                                <div className="text-xs text-gray-500">
                                                                    Priority: {getPriorityLabel(task.priorityOfTask)}
                                                                </div>
                                                            </div>
                                                        )}
                                                    </Draggable>
                                                );
                                            }
                                            return null;
                                        })}
                                        {provided.placeholder}
                                    </div>
                                </div>
                            )}
                        </Droppable>
                    ))}
                </DragDropContext>
            </div>
        </div>
    )
};

// Helper function to get the status label based on the status enum
const getStatusLabel = (status) => {
    switch (status) {
        case StatusType.INPROGRESS:
            return 'In Progress';
        case StatusType.COMPLETED:
            return 'Completed';
        case StatusType.OVERDUE:
            return 'Overdue';
        case StatusType.TODO:
            return 'To do'
        case StatusType.TESTING:
            return 'Testing'
        default:
            return '';
    }
};

// Helper function to get the priority label based on the priority enum
const getPriorityLabel = (priority) => {
    switch (priority) {
        case PriorityType.LOW:
            return 'Low';
        case PriorityType.MEDIUM:
            return 'Medium';
        case PriorityType.HIGH:
            return 'High';
        default:
            return '';
    }
};

export default TaskBoard;