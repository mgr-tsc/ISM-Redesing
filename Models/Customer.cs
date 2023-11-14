namespace ISM_Redesign.Models
{
    public class Customer : Record
    {
        public required int CustomerId { get; set; }

        public required string CustomerName { get; set; }

        public required string CustomerAddress { get; set; }

    }

}