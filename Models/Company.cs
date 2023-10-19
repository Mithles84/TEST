using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEST.Models
{
    public class Company
    {
        [Key]
        public int IdCompany { get; set; }=default(int);
       
        [Required]
        [StringLength(50)]
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Employee Employee { get; set; }
    }
}
