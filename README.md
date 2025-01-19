#WorkToDo

Overview

WorkToDo is a task management application built using ASP.NET Core MVC. It allows users to create, manage, and track tasks with features such as user authentication, task categorization, and dashboard summaries. The app leverages a modular architecture with services for managing users and tasks, making it clean, maintainable, and extensible.

#Features
User Authentication: Users must log in to access their tasks.
Task Management:
Create, edit, delete, and view tasks.
Assign tasks to categories.
Set priorities and due dates.
Dashboard Summary:
Displays task summaries: Pending, Completed, Overdue, and Upcoming tasks.
Dynamic Data Binding:
Categories dynamically loaded for task creation and editing.
Role-Based Access: Only authorized users can access task features.
Scalable Architecture:
Uses IUserService, IUserContext, and ITaskService to separate concerns.

#Technologies Used
Frontend:
Razor Views (HTML, CSS)
Backend:
ASP.NET Core MVC
Entity Framework Core
Database:
Microsoft SQL Server (using Entity Framework migrations)
Authentication:
ASP.NET Identity
Dependency Injection: For services like ITaskService and IUserService.
Logging: Microsoft.Extensions.Logging for debugging and error tracking.


#Setup Instructions
Prerequisites
.NET SDK 7.0+
SQL Server (or an equivalent database engine)
Visual Studio 2022 (recommended) or any other IDE that supports .NET.
Git for version control.
Installation Steps
Clone the Repository:
git clone <repository-url>
cd WorkToDo
Configure the Database:

Update the connection string in appsettings.json:

"ConnectionStrings": {
    "DefaultConnection": "Server=<YOUR_SERVER>;Database=WorkToDoDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
Apply Migrations: Run the following commands to set up the database:
dotnet ef database update
Run the Application:

bash
dotnet run
Access the App: Open your browser and navigate to:
http://localhost:5000


#Project Structure

WorkToDo/
├── Controllers/
│   ├── HomeController.cs        # Handles dashboard and static pages
│   ├── TasksController.cs       # Handles task-related CRUD operations
│   └── AccountController.cs     # Manages user authentication
├── Models/
│   ├── ApplicationUser.cs       # Represents a user in the system
│   ├── WorkItem.cs              # Represents a task
│   └── Category.cs              # Represents a task category
├── Services/
│   ├── IUserService.cs          # Interface for user-related operations
│   ├── TaskService.cs           # Handles task-related operations
│   └── UserContext.cs           # Abstraction for retrieving logged-in user info
├── Views/
│   ├── Home/
│   ├── Tasks/
│   └── Shared/                  # Shared layouts and partial views
├── Data/
│   ├── ApplicationDbContext.cs  # Database context
├── DTO/
│   ├── CreateTaskDTO.cs         # DTO for creating tasks
│   ├── EditTaskDTO.cs           # DTO for editing tasks
│   └── DashboardDTO.cs          # DTO for dashboard data
└── Program.cs                   # Application entry point


#Usage

Task Management
Create a Task:
Navigate to the "Create Task" page and fill out the required fields (title, description, due date, priority, and category).
Edit or Delete Tasks:
Use the edit or delete buttons on the task list or details page.
Dashboard
The dashboard displays a summary of tasks categorized as:
Pending
Completed
Overdue
Upcoming tasks (next 7 days).
Authentication
Log in or register to access personalized task data.
Tasks are linked to the currently logged-in user.

#Future Improvements
Add role-based access control (e.g., Admin and Regular Users).
Enable task reminders via email or notifications.
Add a mobile-friendly design with responsive layouts.
Include unit tests for controllers and services.
Extend the dashboard with charts and visualizations for task analytics.

License
This project is licensed under the MIT License. See the LICENSE file for details.