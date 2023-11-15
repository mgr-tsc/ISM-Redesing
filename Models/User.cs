using ISM_Redesign.Models;

namespace ISM_Redesing.Models
{
    public class User: Record
    {
        public int UserId { get; set; }
        public required string Email { get; set; }
        public required string UserName{ get; set; }

    }

}
