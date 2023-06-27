using System.ComponentModel.DataAnnotations;
namespace BankingSystem.Models
{
    public class Customer
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Fullname { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public int Atmpin { get; set; }
        [Required]
        public string AccountType { get; set; }
        [Required]
        public int AccountNumber { get; set; }
        [Required]
        public string Balance { get; set; }

      
    }

  
        
    
}
