import React, { useState, useEffect, useRef } from 'react';
import { getAutocomplete, searchTasks } from '../../helpers/api';
import { getStatusColor, getPriorityLabel, getStatusLabel } from './TaskBoard';

const SearchForm = () => {
  const [searchQuery, setSearchQuery] = useState('');
  const [selectedStatus, setSelectedStatus] = useState('');
  const [selectedPriority, setSelectedPriority] = useState('');
  const [showAutocomplete, setShowAutocomplete] = useState(false);
  const [autoCompleteOptions, setAutoCompleteOptions] = useState([]);
  const [tasks, setTasks] = useState([]);
  const autocompleteRef = useRef(null);

  const updateTasks = async (query, status, priority) => {
    try {
      const fetchedTasks = await searchTasks(query, status, priority);
      setTasks(fetchedTasks);
    } catch (error) {
      console.error('Error fetching tasks:', error);
    }
  };

  useEffect(() => {
    document.addEventListener('mousedown', handleClickOutsideAutocomplete);

    return () => {
      document.removeEventListener('mousedown', handleClickOutsideAutocomplete);
    };
  }, []);

  // ...

useEffect(() => {
    const fetchFilteredTasks = async () => {
      if(searchQuery.trim() === '')
      {
      setTasks([]);
      return;
      }
      await updateTasks(searchQuery, selectedStatus, selectedPriority);
    };
  
    fetchFilteredTasks();
  }, [searchQuery,selectedStatus, selectedPriority]);
  
  const handleClickOutsideAutocomplete = (e) => {
    if (autocompleteRef.current && !autocompleteRef.current.contains(e.target)) {
      setShowAutocomplete(false);
    }
  };

  const handleSearchInputChange = async (e) => {
    const { value } = e.target;
    setSearchQuery(value);
    setShowAutocomplete(value.trim() !== '');

    if (showAutocomplete) {
      try {
        const options = await getAutocomplete(value);
        setAutoCompleteOptions(options);
      } catch (error) {
        console.error('Error fetching autocomplete options:', error);
      }
    }
  };

  const handleAutocompleteSelection = (value) => {
    setSearchQuery(value);
    setShowAutocomplete(false);
  };

  const handleSearchSubmit = async (e) => {
    e.preventDefault();
    await updateTasks(searchQuery,selectedStatus,selectedPriority);
  };

  const handleAutocompleteKeyDown = (e) => {
    if (e.key === 'ArrowDown' || e.key === 'Tab') {
      e.preventDefault();
      if (autoCompleteOptions.length > 0) {
        const currentIndex = autoCompleteOptions.findIndex((option) => option === searchQuery);
        const nextIndex = currentIndex === autoCompleteOptions.length - 1 ? 0 : currentIndex + 1;
        setSearchQuery(autoCompleteOptions[nextIndex]);
      }
    }
    if(e.key === 'Enter')
    {
        setShowAutocomplete(false);
    }
  };

  return (
    <div className="flex flex-grow h-[calc(100vh-2rem)]">
      <div className="flex flex-col items-center bg-white p-6 rounded-2xl w-full">
        <div className="flex items-center mb-4">
          <select
            value={selectedStatus}
            onChange={(e) => setSelectedStatus(e.target.value)}
            className="px-4 py-2 border border-gray-300 rounded-md"
          >
            <option value="">All Status</option>
            <option value="0">To do</option>
            <option value="1">In Progress</option>
            <option value="2">Testing</option>
            <option value="3">Completed</option>
            <option value="4">Overdue</option>
            {/* Add status options here */}
          </select>
          <select
            value={selectedPriority}
            onChange={(e) => setSelectedPriority(e.target.value)}
            className="ml-2 px-4 py-2 border border-gray-300 rounded-md"
          >
            <option value="">All Priority</option>
            <option value="1">Low</option>
            <option value="2">Medium</option>
            <option value="3">High</option>
          </select>
        </div>
        <form onSubmit={handleSearchSubmit} className="flex items-center mb-4">
          <div className="relative">
            <input
              type="text"
              value={searchQuery}
              onChange={handleSearchInputChange}
              onKeyDown={handleAutocompleteKeyDown}
              onBlur={() => setShowAutocomplete(false)}
              placeholder="Search tasks"
              className="px-4 py-2 border border-gray-300 rounded-md"
            />
            {showAutocomplete && (
              <div ref={autocompleteRef} className="absolute z-10 w-full mt-2 bg-white border border-gray-300 rounded-md shadow-lg">
                {autoCompleteOptions.map((option) => (
                  <div
                    key={option}
                    className="px-4 py-2 cursor-pointer hover:bg-gray-100"
                    onClick={() => handleAutocompleteSelection(option)}
                  >
                    {option}
                  </div>
                ))}
              </div>
            )}
          </div>
          <button
            type="submit"
            className="ml-2 px-4 py-2 bg-blue-500 text-white rounded-md"
          >
            Search
          </button>
        </form>
        <div className="flex flex-wrap gap-4">
          {tasks.map((task) => (
            <div key={task.id} className="bg-white p-2 rounded shadow hover:animate-pulse w-64">
              <div className="flex items-center space-x-2">
                <div className={`bg-${getStatusColor(task.status)} p-2 rounded shadow hover:animate-pulse`} />
                <span>{task.title}</span>
              </div>
              <div className="text-xs text-gray-500"><b>Description:</b> <i>{task.description}</i></div>
              <div className="text-xs text-gray-500"><b>Priority:</b>: {getPriorityLabel(task.priorityOfTask)}</div>
              <div className="text-xs text-gray-500"><b>Status:</b> {getStatusLabel(task.status)}</div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default SearchForm;
