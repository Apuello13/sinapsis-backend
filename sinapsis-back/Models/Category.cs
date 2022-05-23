using System.ComponentModel.DataAnnotations;

namespace sinapsis_back.Models
{
    public class Category
    {
        [Key]
        public int IdCategory { get; set; }
        public string Name { get; set; }
    }
}
