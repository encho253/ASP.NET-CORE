using System.ComponentModel.DataAnnotations;

namespace CatsServer.Models
{
    public class Cat
    {
        private const int StringMaxLength = 50;

        [Required]
        [MaxLength(50)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0, 30)]
        public int Age { get; set; }

        [Required]
        [MaxLength(50)]
        public string Breed { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Image { get; set; }
    }
}