using SFTEST.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SFTEST.DAO
{
    public class EmployeeContext: DbContext
    {
        public EmployeeContext() : base("EmployeeContext")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Employee>()
               .HasOptional(e => e.Tasks)
               .WithMany()
               .WillCascadeOnDelete(true);
            base.OnModelCreating(modelBuilder);
        }
    }
}