import React from "react";
import Sidebar from "../components/dashboard/Sidebar";
import SearchForm from "../components/dashboard/SearchForm";

const Search = () => {
  return (
    <div className="flex">
      <div className="w-1/4">
        <Sidebar />
      </div>
      <div className="w-3/4">
        <SearchForm />
      </div>
    </div>
  );
};

export default Search;
