using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEST.Models
{
    public class Employee
    {
        [Key]
        public int IdEmployee { get; set; }

        [Required(ErrorMessage = "Name  is required  with maximum length 50")]
        [StringLength(50)]
        public string  EmployeeName { get; set; }

        [Range(1, 100000000, ErrorMessage = "Salary greater than 1 Rs")]
        public int Salary { get; set; }

        //[ForeignKey("CompanyId")]
        public int CompanyId { get; set; }

    }
}
