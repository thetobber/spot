using System.Collections.Generic;

namespace Spot.Models.Generic.ViewModels
{
    public class PagedViewModel<TEntity>
    {
        public int Pages { get; set; }

        public int Index { get; set; }

        public bool Next => Index < Pages;

        public bool Previous => Index > 1 && Index <= Pages;

        public ICollection<TEntity> Entities { get; set; }
    }
}