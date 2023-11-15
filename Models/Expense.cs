using ISM_Redesign.Models;
namespace ISM_Redesing.Models
{
    public class Expense : Record
    {
        public int ExpenseID { get; set; }
        public string? Description  { get; set; }
        public decimal Amount { get; set; }
    }
}