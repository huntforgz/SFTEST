# SFTEST
A simple web page for employees management using ASP.NET MVC5 with .NET Framework 4.72

# Design
 - Entity Framework 6+ as ORM ,Ninject as IOC container to inject Dbcontext to controller
### Domain
* Employee
  - One to many relationship with Task
  - Name string length < 50
  - Date format YYYY-MM-DD(then it would display during editting)
* Task
  - Deadline date should be no less than current day
  - Dealline date should be no less than start time
  
### DB
* SQL Server Express LocalDB
 
### Controller
* Employee
  - Homepage(display for all employees),create, edit, delete
  - For add or delete employe tasks,using check box to select or remove
  
* Task
  - Homepage(display for all employees),create, edit, delete
  - Add contraint for datetime type properties and corresponding error message if encounters invalidated value
  
### Unit Test

