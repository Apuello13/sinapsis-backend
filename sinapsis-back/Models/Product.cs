namespace sinapsis_back.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int IdMark { get; set; }
        public int IdCategory { get; set; }
        public double Price { get; set; }
        public DateTime DateExpiry { get; set; }
        public double Taxes { get; set; }
        public virtual Mark? TypeProduct { get; set; }
        public virtual Category? Category { get; set; }
    }
}
