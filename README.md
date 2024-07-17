
Student Management Desktop Application Readme
Overview:
This desktop application is developed using C#, WCF (Windows Communication Foundation), and SQL Server. It provides comprehensive management functionalities for student and department data within an educational institution.

Features:
Department Management:

CRUD operations for managing departments.
View, add, edit, and delete departments.
Student Management:

CRUD operations for managing student records.
View, add, edit, and delete student information.
Search functionality to find students by various criteria.
Grade Management:

Manage student grades for different subjects or courses.
Add, edit, and delete grades.
Export Data:

Export student and department data to Excel format.
Export student records to PDF format.
User Interface:

Intuitive desktop application interface designed for ease of use.
Clear navigation between different modules (departments, students, grades).
Database Integration:

Utilizes SQL Server for robust data storage and management.
Entity Framework (EF) for seamless database operations.
Setup Instructions:
Prerequisites:

Visual Studio (recommended version: Visual Studio 2019 or later)
SQL Server (Express edition or higher)
.NET Framework (version 4.5 or later)
Database Setup:

Create a new database in SQL Server.
Execute the SQL scripts provided in the DatabaseScripts folder to create tables and initial data.
Application Configuration:

Open the solution in Visual Studio.
Update the connection string in App.config file to point to your SQL Server database.
Build and Run:

Build the solution.
Run the application in debug mode from Visual Studio.
Usage:
Upon launching the application, you will be presented with a login screen.
Use appropriate credentials to log in (if authentication is implemented).
Navigate through different modules (Departments, Students, Grades) using the sidebar or menu.
Perform CRUD operations on departments and students as needed.
Export data to Excel or PDF using the respective buttons provided.
Future Enhancements:
Authentication and Authorization: Implement role-based access control for different users.
Reporting: Integrate reporting capabilities for generating detailed student reports.
UI/UX Improvements: Enhance user interface for better usability and accessibility.
Performance Optimization: Optimize database queries and application performance.
