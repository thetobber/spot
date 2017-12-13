using System.ComponentModel.DataAnnotations;

namespace Spot.Models.Post.ViewModels
{
    public class PostEditViewModel : PostNewViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}