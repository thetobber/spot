using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spot.Models.Tag
{
    public class TagModel
    {
        public int Id { get; set; }

        [DataType(DataType.Text), StringLength(255)]
        public string Name { get; set; }
    }
}