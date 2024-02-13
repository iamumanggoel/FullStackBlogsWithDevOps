using System.ComponentModel.DataAnnotations;

namespace BlogServices.Models.DTOs
{
    public class BlogRequest
    {

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(100, ErrorMessage = "Title must not exceed 100 characters.")]
        public string Title { get; set; }


        [MaxLength(500, ErrorMessage = "ShortDescription must not exceed 500 characters.")]
        public string ShortDescription { get; set; }


        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; }


        [Url(ErrorMessage = "FeaturedImageUrl must be a valid URL.")]
        public string FeaturedImageUrl { get; set; }


        [Required(ErrorMessage = "PublishedDate is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "PublishedDate must be a valid DateTime.")]
        [DisplayFormat(DataFormatString = "{0:yyyy:MM:dd : HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime PublishedDate { get; set; }


        [Required(ErrorMessage = "Author is required.")]
        [MaxLength(50, ErrorMessage = "Author must not exceed 50 characters.")]
        public string Author { get; set; }

        //instead of directly making this foreign key, i will make sure it is when sending request with frontend
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string UserEmail { get; set; }
    }
}
