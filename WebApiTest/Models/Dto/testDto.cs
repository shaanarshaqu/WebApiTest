using System.ComponentModel.DataAnnotations;

namespace WebApiTest.Models.Dto
{
    public class testDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
    }
}
