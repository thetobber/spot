using System;
using System.ComponentModel.DataAnnotations;
using Spot.Models.Post;

namespace Spot.Models.Comment
{
    public class CommentModel
    {
        public int Id { get; set; }
        
        public PostModel PostId { get; set; }

        [DataType(DataType.Html)]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }
    }
}