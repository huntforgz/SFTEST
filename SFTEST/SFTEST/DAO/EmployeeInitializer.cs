using SFTEST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFTEST.DAO
{
    public class EmployeeInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<EmployeeContext>
    {
        
        protected override void Seed(EmployeeContext context)
        {
            var employees = new List<Employee>
            {
                new Employee{FirstName="Hunt1",LastName="Huang1",HiredDate=DateTime.Parse("2020-12-01")},
                new Employee{FirstName="Hunt2",LastName="Huang2",HiredDate=DateTime.Parse("2020-12-01")},
            };

            employees.ForEach(e => context.Employees.Add(e));
            context.SaveChanges();

            var tasks = new List<Task>
            {
                new Task{TaskName="Task1", StartTime=DateTime.Parse("2020-12-01"), Deadline = DateTime.Parse("2020-12-05"),},
                new Task{TaskName="Task2", StartTime=DateTime.Parse("2020-12-01"), Deadline = DateTime.Parse("2020-12-05"),},
                new Task{TaskName="Task3", StartTime=DateTime.Parse("2020-12-01"), Deadline = DateTime.Parse("2020-12-05"),},
                new Task{TaskName="Task4", StartTime=DateTime.Parse("2020-12-01"), Deadline = DateTime.Parse("2020-12-05"),},
            };
            tasks.ForEach(t => context.Tasks.Add(t));
            context.SaveChanges();
            

        }

    }
}