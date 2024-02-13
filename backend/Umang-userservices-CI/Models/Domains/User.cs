using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace UserServices.Models.Domains
{
    public class User
    {
        [Required]
        [MaxLength(50, ErrorMessage = "The maximum length for FirstName is 50 characters.")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The maximum length for LastName is 50 characters.")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(15, ErrorMessage = "The maximum length for PhoneNumber is 15 characters.")]
        [RegularExpression(@"^\+?[0-9]*$", ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DOB { get; set; }

        [Required]
        [Key]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "The password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "The password must have at least one lowercase letter, one uppercase letter, and one digit.")]
        public string Password { get; set; }
    }
}
