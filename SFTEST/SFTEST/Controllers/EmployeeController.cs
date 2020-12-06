using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SFTEST.DAO;
using SFTEST.Models;
using SFTEST.ViewModels;

namespace SFTEST.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeContext db;

        // GET: Employee
        public EmployeeController( EmployeeContext db)
        {
            this.db = db;
        }
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,HiredDate")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            PopulateAssignedTaskData(employee);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        private void PopulateAssignedTaskData(Employee employee)
        {
            var allTasks = db.Tasks;
            var employeeTasks = new HashSet<int>();
            if (employee.Tasks != null)
            {
                employeeTasks = new HashSet<int>(employee.Tasks.Select(c => c.ID));

            }
            
            var viewModel = new List<AssignedTaskData>();
            foreach (var task in allTasks)
            {
                viewModel.Add(new AssignedTaskData
                {
                    TaskID = task.ID,
                    TaskName = task.TaskName,
                    Assigned = employeeTasks.Contains(task.ID)
                });
            }
            ViewBag.Tasks = viewModel;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedTasks)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);

            if (TryUpdateModel(employee, "",
               new string[] { "FirstName", "LastName", "HiredDate" }))
            {
                try
                {
                    UpdateEmployeeTasks(selectedTasks, employee);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAssignedTaskData(employee);
            return View(employee);
        }
        private void UpdateEmployeeTasks(string[] selectedTasks, Employee employee)
        {
            if (selectedTasks == null)
            {
                employee.Tasks = new List<Task>();
                return;
            }

            var selectedTasksHS = new HashSet<string>(selectedTasks);
            var employeeTasks = new HashSet<int>();
            if (employee.Tasks != null)
            {
                employeeTasks = new HashSet<int>
                (employee.Tasks.Select(c => c.ID));
            }
            else
            {
                employee.Tasks = new List<Task>();
            }
            foreach (var task in db.Tasks)
            {
                if (selectedTasksHS.Contains(task.ID.ToString()))
                {
                    if (!employeeTasks.Contains(task.ID))
                    {
                        employee.Tasks.Add(task);
                    }
                }
                else
                {
                    if (employeeTasks.Contains(task.ID))
                    {
                        employee.Tasks.Remove(task);
                    }
                }
            }
        }


        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
