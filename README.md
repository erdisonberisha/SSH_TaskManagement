<!DOCTYPE html>
<html>

<body>
  <h1>SSH - Task Management Project</h1>

  <p>This repository contains a Task Management project that consists of a .NET API and a React frontend application. The API utilizes Entity Framework Core and communicates with a PostgreSQL database using a Docker Compose file. Follow the instructions below to set up and run the project.</p>

  <h2>Prerequisites</h2>

  <p>Before running the project, ensure that you have the following software installed on your system:</p>

  <ul>
    <li><a href="https://nodejs.org">Node.js</a></li>
    <li><a href="https://dotnet.microsoft.com/download">.NET SDK</a></li>
    <li><a href="https://www.docker.com/get-started">Docker Desktop</a></li>
  </ul>

  <h2>Running the API</h2>

  <ol>
    <li>Open a terminal and navigate to the <code>TaskManagementAPI</code> folder.</li>
    <li>Start the PostgreSQL database by running the following command:<br>
      <code>docker-compose up -d</code></li>
    <li>Once the database is running, open the solution in Visual Studio or any compatible .NET IDE.</li>
    <li>In the Package Manager Console, execute the following command to apply the database migrations:<br>
      <code>update-database</code><br>
      This will create the necessary tables in the PostgreSQL database.</li>
    <li>Build and run the API project.</li>
  </ol>

  <p>The API is now up and running, and it will be accessible at <a href="https://localhost:7183/">https://localhost:7183/</a>.</p>

  <h2>Running the Frontend</h2>

  <ol>
    <li>Open a terminal and navigate to the <code>frontend</code> folder.</li>
    <li>Install the required dependencies by running the following command:<br>
      <code>npm install</code></li>
    <li>Once the installation is complete, start the React application by running:<br>
      <code>npm run start</code></li>
  </ol>

  <p>The frontend application will now be accessible at <a href="http://localhost:3000">http://localhost:3000</a>.</p>

  <p>You're all set! You can now access the Task Management application through your browser and start using it.</p>

  <h2>Troubleshooting</h2>

  <ul>
    <li>If you encounter any issues while running the project, make sure you have met all the prerequisites and followed the instructions correctly.</li>
    <li>Check the console output for any error messages or logs that can help identify the problem.</li>
    <li>If you need further assistance, feel free to reach out to the project's maintainers.</li>
  </ul>

</body>

</html>
