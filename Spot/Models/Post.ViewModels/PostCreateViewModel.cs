using System.ComponentModel.DataAnnotations;

namespace Spot.Models.Post.ViewModels
{
    public class PostCreateViewModel
    {
        [Required]
        public PostStatus Status { get; set; }

        [DataType(DataType.Text)]
        [Required]
        [MinLength(10)]
        [MaxLength(255)]
        public string Title { get; set; }

        [DataType(DataType.Text)]
        [Required]
        [MinLength(10)]
        [MaxLength(255)]
        public string Excerpt { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}
