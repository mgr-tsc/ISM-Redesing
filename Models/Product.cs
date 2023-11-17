
namespace ISM_Redesign.Models
{
    public class Product : Record
    {

        public int ProductID { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Area { get; set; }
        public required long QuantityInStock { get; set; }
        public required ProductUnitMeasure UnitMeasure { get; set; }
        public required string StockUniqueID { get; set; }
    }
    public enum ProductUnitMeasure
    {
        Lb,
        Caja,
        Kg,
        Litro,
        Galon,
    }
}