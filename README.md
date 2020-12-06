# SFTEST
A simple web page for employees management using ASP.NET MVC5 with .NET Framework 4.72

# Design

### Domain
* Employee
  - One to many relationship with Task
  - Name string length < 50
  - Date format YYYY-MM-DD
* Task
  - Deadline date should be no less than current day
  - Dealline date should be no less than start time

