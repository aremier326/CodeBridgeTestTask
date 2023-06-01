using System.ComponentModel.DataAnnotations;

namespace CodeBridgeTestTask.Models
{
    public class Dog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Color { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int TailLength { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Weight { get; set; }

    }
}
