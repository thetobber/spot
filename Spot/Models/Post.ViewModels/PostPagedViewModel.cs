using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spot.Models.Post.ViewModels
{
    public class PostPagedViewModel
    {
        public int Count { get; set; }

        public int Index { get; set; }

        public int Size { get; set; }

        public bool HasNext => (Index + 1) * Size % Count == 0;

        public bool HasPrevious => (Index - 1) * Size % Count == 0;

        public IEnumerable<PostModel> Posts { get; set; }
    }
}