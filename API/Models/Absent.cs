using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_M_Absent")]
    public class Absent
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Employee")]
        public string UserId { get; set; }
        public DateTimeOffset InsDate { get; set; }
        public DateTimeOffset UpdDate { get; set; }

        public Employee Employee { get; set; }
    }
}
