using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Spot.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [DataType(DataType.Text), StringLength(255)]
        public string Name { get; set; }

        //public IEnumerable<Post> Posts { get; set; }
    }
}