using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Spot.Models.Tag;

namespace Spot.Models.Post.ViewModels
{
    public class PostExcerptViewModel
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Excerpt { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Modified { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Published { get; set; }

        public ICollection<TagModel> Tags { get; set; }
    }
}
