import React, { useState, useEffect } from 'react';
import { DragDropContext, Droppable, Draggable } from 'react-beautiful-dnd';
import { getTasks, deleteTask } from '../helpers/api';
import CreateTaskForm from './CreateTask';
import { updateTaskStatus } from '../helpers/api';
import { Button } from '@material-tailwind/react';
// Enums
export const StatusType = {
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
  const [showCreateTaskForm, setShowCreateTaskForm] = useState(false);
  const [selectedStatus, setSelectedStatus] = useState('');
  const [taskList, setTaskList] = useState([]);


  const handleCreateTask = (status) => {
    setSelectedStatus(status);
    setShowCreateTaskForm(true);
  };

  const handleDeleteTask = async (taskId) => {
    try {
      await deleteTask(taskId);
      const updatedTasks = taskList.filter(task => task.id !== taskId);
      setTaskList(updatedTasks);
    } catch (err) {
      console.log(err);
    }
  };

  const handleTaskFormSubmit = (newTaskData) => {
    // ...your code for handling the submission of the new task form...
  };

  const handleTaskFormClose = () => {
    setShowCreateTaskForm(false);
  };

  useEffect(() => {
    const fetchTasks = async () => {
      try {
        const tasks = await getTasks();
        setTaskList(tasks);
      } catch (error) {
        console.log('Error fetching tasks!')
      }
    };
    fetchTasks();
  }, []);

  const onDragEnd = async (result) => {
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

    try {
      await updateTaskStatus(draggedTask.id, draggedTask.status);
      console.log('Task status updated successfully');
    } catch (error) {
      console.log('Error updating task status:', error.message);
      // Revert the task list back to the original state on error
      setTaskList(taskList);
    }
  };

  const getColumnWidth = () => {
    const columnCount = Object.values(StatusType).length;
    const containerWidth = 100; // Percentage of container width
    const gutterWidth = 2; // Percentage of gutter width
    const columnWidth = (containerWidth - (columnCount - 1) * gutterWidth) / columnCount;
    return columnWidth;
  };


  return (
    <div className="flex flex-grow h-[calc(100vh-2rem)]">
      <div className="flex space-x-4 bg-white p-6 rounded-2xl w-full">
        <DragDropContext onDragEnd={onDragEnd}>
          {Object.values(StatusType).map((status) => (
            <Droppable key={status} droppableId={`status-${status}`} type="TASK">
              {(provided) => (
                <div
                  className="flex flex-col min-w-0"
                  style={{
                    width: `${getColumnWidth()}%`,
                    overflowY: 'auto',
                    maxHeight: 'calc(100vh - 56px)',
                  }}
                >
                  <div className="flex items-center justify-between mb-2">
                    <span className="font-semibold">{getStatusLabel(status)}</span>
                    <button
                      className="px-2 py-1 text-sm bg-blue-500 text-white rounded"
                      onClick={() => handleCreateTask(status)}
                    >
                      Add Task
                    </button>
                  </div>
                  <div
                    className="bg-gray-100 rounded-xl shadow p-2 space-y-2 flex-grow"
                    {...provided.droppableProps}
                    ref={provided.innerRef}
                  >
                    {taskList.map((task, index) => {
                      if (task.status === status) {
                        return (
                          <Draggable key={task.id} draggableId={`task-${task.id}`} index={index}>
                            {(provided) => (
                              <div
                                className="bg-white p-2 rounded shadow hover:animate-pulse relative"
                                {...provided.draggableProps}
                                {...provided.dragHandleProps}
                                ref={provided.innerRef}
                              >
                                <div className="flex items-center space-x-2">
                                  <div className={`bg-${getStatusColor(task.status)} p-2 rounded shadow hover:animate-pulse`} />
                                  <span>{task.title}</span>
                                </div>
                                <div className="text-xs text-gray-500"><b>Description:</b> <i>{task.description}</i></div>
                                <div className="text-xs text-gray-500"><b>Priority:</b> {getPriorityLabel(task.priorityOfTask)}</div>
                                <Button
                                  color="red"
                                  size="sm"
                                  onClick={() => handleDeleteTask(task.id)}
                                  className='!absolute -top-1 -right-1 rounded-full !p-1 !pl-2 !pr-2 text-xs'
                                >
                                  <i className="fas fa-close"></i>
                                </Button>
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
      {showCreateTaskForm && (
        <div className="fixed inset-0 flex items-center justify-center bg-gray-900 bg-opacity-50">
          <div className="z-10 w-96">
            <CreateTaskForm
              status={selectedStatus}
              onSubmit={handleTaskFormSubmit}
              onClose={handleTaskFormClose}
            />
          </div>
        </div>
      )}
    </div>
  );
};
// Helper function to get the status label based on the status enum
export const getStatusLabel = (status) => {
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
export const getPriorityLabel = (priority) => {
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

export const getStatusColor = (status) => {
  switch (status) {
    case 0:
      return 'gray-500';
    case 1:
      return 'yellow-500';
    case 2:
      return 'blue-500';
    case 3:
      return 'green-500';
    case 4:
      return 'red-500';
    default:
      return 'blue-500'; // Default color if status value doesn't match any case
  }
};


export default TaskBoard;