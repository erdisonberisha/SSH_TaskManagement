import React from "react";
import Sidebar from "../components/Sidebar";
import TaskBoard from "../components/TaskBoard";

const Dashboard = () => {
  return (
    <div className="flex">
      <div className="w-1/4">
        <Sidebar />
      </div>
      <div className="w-3/4">
        <TaskBoard />
      </div>
    </div>
  );
};

export default Dashboard;
