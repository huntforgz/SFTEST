using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFTEST.ViewModels
{
    public class AssignedTaskData
    {
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public bool Assigned { get; set; }
    }
}