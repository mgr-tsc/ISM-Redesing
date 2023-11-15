
namespace ISM_Redesign.Models
{
    public class Product : Record
    {
        public int ProductID { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}