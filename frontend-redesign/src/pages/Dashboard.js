import React from "react";
import Sidebar from "../components/Sidebar";
import TaskBoard from "../components/TaskBoard";

const Dashboard = () => {
  return (
    <div className="flex">
      <div class="w-1/4">
        <Sidebar />
      </div>
      <div class="w-3/4">
        <TaskBoard />
      </div>
    </div>
  );
};

export default Dashboard;