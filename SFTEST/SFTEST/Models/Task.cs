using SFTEST.CustomerAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SFTEST.Models
{
    public class Task
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Display(Name = "Task Name")]
        public string TaskName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [CustomerDate(ErrorMessage = "Back date entry not allowed")]
        [DateGreaterThanAttribute(otherPropertyName = "StartTime", ErrorMessage = "Deadline must be equal or greater than start time.")]
        [Display(Name = "Deadline")]
        public DateTime Deadline { get; set; }

     
    }
}