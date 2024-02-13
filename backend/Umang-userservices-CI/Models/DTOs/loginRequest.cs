using System.ComponentModel.DataAnnotations;

namespace UserServices.Models.DTOs
{
    public class loginRequest
    {
        [Required]
        [Key]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
