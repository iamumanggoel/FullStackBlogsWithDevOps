using System.ComponentModel.DataAnnotations;

namespace CommentServices.Models.DTOs
{
    public class CommentRequest
    {
       
        [StringLength(100, MinimumLength = 2, ErrorMessage = "CommentText must be between 2 and 100 characters.")]
        public string CommentText { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string UserEmail { get; set; }

        [Required]
        public Guid BlogId { get; set; }

        [Required]
        public string Name { get; set; }
    }

}
