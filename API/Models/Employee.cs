﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_M_Employee")]
    public class Employee
    {
        public string EmpId { get; set; }
        public string Name { get; set; }
        public string NIK { get; set; }
        public string AssignmentSite { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTimeOffset CreateData { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public DateTimeOffset DeleteData { get; set; }
        public bool isDelete { get; set; }
        public User User { get; set; }
    }
}
