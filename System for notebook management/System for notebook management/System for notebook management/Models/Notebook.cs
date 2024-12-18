﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectR.Models
{
    public class Notebook
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(NotebookSpecifications))]
        public int NotebookSpecificationsId { get; set; }
        public NotebookSpecifications NotebookSpecifications { get; set; }

        [ForeignKey(nameof(Company))]
        public int CompanyId { get; set; }

        public Company Company { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Type { get; set; }
        public int NumberOfPages { get; set; }
        public double Price { get; set; }
        [StringLength(1000)]
        public string CoverImage { get; set; }
        public bool IsWithLines { get; set; }
        [StringLength(50)]
        public string PaperType { get; set; }
    }
}
