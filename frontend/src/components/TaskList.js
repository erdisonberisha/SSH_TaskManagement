import React from 'react';
import { Card, CardBody, CardFooter } from '@material-tailwind/react';
import { DragDropContext, Droppable, Draggable } from 'react-beautiful-dnd';

const TaskList = ({ tasks, onDragEnd }) => {
  const taskGroups = {
    'Not Started': [],
    'In Progress': [],
    Finished: [],
  };

  tasks.forEach((task) => {
    taskGroups[task.status].push(task);
  });

  return (
    <DragDropContext onDragEnd={onDragEnd}>
      <div className="flex overflow-x-auto">
        {Object.entries(taskGroups).map(([status, taskList]) => (
          <div key={status} className="flex flex-col space-y-4">
            <h2 className="font-bold text-lg">{status}</h2>
            <div className="max-h-96 overflow-y-auto">
              <Droppable droppableId={status}>
                {(provided) => (
                  <div
                    className="flex flex-col space-y-4"
                    ref={provided.innerRef}
                    {...provided.droppableProps}
                  >
                    {taskList.map((task, index) => (
                      <Draggable
                        key={task.id}
                        draggableId={task.id.toString()}
                        index={index}
                      >
                        {(provided) => (
                          <div
                            ref={provided.innerRef}
                            {...provided.draggableProps}
                            {...provided.dragHandleProps}
                          >
                            <Card>
                              <CardBody>
                                <h5 className="card-title">{task.title}</h5>
                                <p className="text-gray-500">{task.description}</p>
                                <p className="text-gray-500">Status: {task.status}</p>
                              </CardBody>
                              <CardFooter>
                                <p className="text-sm text-gray-500">
                                  Deadline: {task.deadline}
                                </p>
                              </CardFooter>
                            </Card>
                          </div>
                        )}
                      </Draggable>
                    ))}
                    {provided.placeholder}
                  </div>
                )}
              </Droppable>
            </div>
          </div>
        ))}
      </div>
    </DragDropContext>
  );
};

export default TaskList;
