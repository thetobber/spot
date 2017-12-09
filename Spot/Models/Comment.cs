using System;
using System.ComponentModel.DataAnnotations;

namespace Spot.Models
{
    public class Comment
    {
        public int Id { get; set; }
        
        public Post PostId { get; set; }

        [DataType(DataType.Html)]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }
    }
}