using System.ComponentModel.DataAnnotations;

namespace sinapsis_back.Models
{
    public class Mark
    {
        [Key]
        public int IdMark { get; set; }
        public string Name { get; set; }
    }
}
