using System.ComponentModel.DataAnnotations;

namespace ProjectR.Models
{
    public class NotebookSpecifications
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double Weight { get; set; }
        public bool IsRecyclable { get; set; }
        public bool HasRuler { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Material { get; set; }
        public string CoverColor { get; set; }

    }
}
