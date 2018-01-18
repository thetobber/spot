using System;
using System.ComponentModel.DataAnnotations;
using Spot.Models.Category;
using Spot.Models.User;

namespace Spot.Models.Post
{
    public class PostModel
    {
        public int Id { get; set; }

        public PostStatus Status { get; set; }

        [DataType(DataType.Text)]
        [StringLength(255)]
        public string Title { get; set; }

        [DataType(DataType.Html)]
        public string Content { get; set; }

        [DataType(DataType.Text)]
        [StringLength(255)]
        public string Excerpt { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Modified { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Published { get; set; }

        // public UserModel Author { get; set; }
        public string Author { get; set; }

        // public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
    }
}
