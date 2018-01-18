using System;
using System.Collections.Generic;
using Spot.Models.Category;

namespace Spot.Models.Post.ViewModels
{
    public class PostExcerptViewModel
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Excerpt { get; set; }

        public DateTime? Published { get; set; }

        public CategoryModel Category { get; set; }
    }
}
