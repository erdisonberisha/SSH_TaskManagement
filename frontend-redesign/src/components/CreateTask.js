import { useState, useRef } from 'react';
import { StatusType, getStatusLabel } from './TaskBoard';

const CreateTaskForm = ({ status, onSubmit, onClose }) => {
    const [newTaskData, setNewTaskData] = useState({
        title: '',
        description: '',
        dueDate: '',
        status: status || '',
        priority: '',
        invitedUser: '',
    });
    const formRef = useRef(null);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setNewTaskData((prevState) => ({
            ...prevState,
            [name]: value,
        }));
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        onSubmit(newTaskData);
        setNewTaskData({
            title: '',
            description: '',
            dueDate: '',
            status: status || '',
            priority: '',
            invitedUser: '',
        });
    };

    return (
        <div className="bg-white rounded-md shadow p-5" ref={formRef}>
            <form onSubmit={handleSubmit}>
                <div className="mb-4">
                    <label className="block mb-1 font-semibold">Title</label>
                    <input
                        type="text"
                        name="title"
                        value={newTaskData.title}
                        onChange={handleInputChange}
                        className="w-full px-2 py-1 border border-gray-300 rounded"
                        required
                    />
                </div>
                <div className="mb-4">
                    <label className="block mb-1 font-semibold">Description</label>
                    <textarea
                        name="description"
                        value={newTaskData.description}
                        onChange={handleInputChange}
                        className="w-full px-2 py-1 border border-gray-300 rounded"
                        required
                    ></textarea>
                </div>
                <div className="mb-4">
                    <label className="block mb-1 font-semibold">Due Date</label>
                    <input
                        type="datetime-local"
                        name="dueDate"
                        value={newTaskData.dueDate}
                        onChange={handleInputChange}
                        className="w-full px-2 py-1 border border-gray-300 rounded"
                        required
                    />
                </div>
                <div className="mb-4">
                    <label className="block mb-1 font-semibold">Status</label>
                    <select
                        name="status"
                        value={newTaskData.status}
                        onChange={handleInputChange}
                        className="w-full px-2 py-1 border border-gray-300 rounded"
                        required
                    >
                        <option value="">Select Status</option>
                        {Object.values(StatusType).map((status) => (
                            <option key={status} value={status}>
                                {getStatusLabel(status)}
                            </option>
                        ))}
                    </select>
                </div>
                <div className="mb-4">
                    <label className="block mb-1 font-semibold">Priority</label>
                    <select
                        name="priority"
                        value={newTaskData.priority}
                        onChange={handleInputChange}
                        className="w-full px-2 py-1 border border-gray-300 rounded"
                        required
                    >
                        <option value="">Select Priority</option>
                        <option value="low">Low</option>
                        <option value="medium">Medium</option>
                        <option value="high">High</option>
                    </select>
                </div>
                <div className="flex justify-between">
                    <button
                        type="submit"
                        className="px-4 py-2 text-sm bg-blue-500 text-white rounded"
                    >
                        Create Task
                    </button>
                    <button
                        type="button"
                        className="px-4 py-2 text-sm bg-gray-500 text-white rounded"
                        onClick={onClose}
                    >
                        Cancel
                    </button>
                </div>
            </form>
        </div>
    );
};

export default CreateTaskForm;
