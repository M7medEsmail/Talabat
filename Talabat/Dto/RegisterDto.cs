using System.ComponentModel.DataAnnotations;

namespace Talabat.Dto
{
    public class RegisterDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        //[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*])[A-Za-z\\d!@#$%^&*]{8,}$\r\n" , 
        //    ErrorMessage = "At least 8 characters long, Contains at least one uppercase letter, Contains at least one lowercase letter, Contains at least one digit, Contains at least one special character (e.g., !@#$%^&*)")]
        public string Password { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        
    }
}
