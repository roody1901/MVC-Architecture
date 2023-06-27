using System.ComponentModel.DataAnnotations;
namespace BankingSystem.Models
{
    public class Admin
    {

        [Required]
        public int Id { get; set; }

        [Required]        
        public int Passwords { get; set; }
    }
}
