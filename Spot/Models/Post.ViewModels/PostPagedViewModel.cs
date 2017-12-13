using System.Collections.Generic;

namespace Spot.Models.Post.ViewModels
{
    public class PostPagedViewModel
    {
        public int Count { get; set; }

        public int Index { get; set; }

        public int Size { get; set; }

        public bool HasNext => (Index + 1) * Size % Count == 0;

        public bool HasPrevious => (Index - 1) * Size % Count == 0;

        public ICollection<PostModel> Posts { get; set; }
    }
}