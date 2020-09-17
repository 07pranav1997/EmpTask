using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpTask.Models
{
    public class EmployeeModel
    {
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string Qualification { get; set; }
        public string Designation { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string ReportingManager { get; set; }
        public string Department { get; set; }
    }
}