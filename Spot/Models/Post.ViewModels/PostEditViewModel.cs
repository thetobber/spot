using System.ComponentModel.DataAnnotations;

namespace Spot.Models.Post.ViewModels
{
    public class PostEditViewModel : PostCreateViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}