using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Spot.Models.Category;
using Spot.Models.Comment;
using Spot.Models.User;

namespace Spot.Models.Post
{
    public class PostModel
    {
        public int Id { get; set; }

        public PostStatus Status { get; set; }

        [DataType(DataType.Text), StringLength(255)]
        public string Title { get; set; }

        [DataType(DataType.Html)]
        public string Content { get; set; }

        [DataType(DataType.Text), StringLength(255)]
        public string Excerpt { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Modified { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Published { get; set; }

        public UserModel Author { get; set; }

        public CategoryModel Category { get; set; }

        public ICollection<CommentModel> Comments { get; set; }
    }
}
