using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectR.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public ICollection<Notebook> NotebookCollection { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public DateTime YearOfCreation { get; set; }
        public int companyMembers { get; set; }
        public double AnnualRevenue { get; set; }
        public long SoldNotebooks { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

    }
}
