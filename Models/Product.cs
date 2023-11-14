
using ISM_Redesign.Models;

namespace ISM_Redesign.Models
{
    public class Product : Record
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}