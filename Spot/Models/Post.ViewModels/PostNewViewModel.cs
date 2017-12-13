using System.ComponentModel.DataAnnotations;

namespace Spot.Models.Post.ViewModels
{
    public class PostNewViewModel
    {
        [Required]
        public PostStatus Status { get; set; }

        [Required(ErrorMessage = "The title is required.")]
        [DataType(DataType.Text)]
        [StringLength(255, ErrorMessage = "The title must have a length of maximum 255 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The excerpt is required.")]
        [DataType(DataType.Text)]
        [StringLength(255, ErrorMessage = "The excerpt must have a length of maximum 255 characters.")]
        public string Excerpt { get; set; }

        [Required(ErrorMessage = "The content is required.")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}
