using System.ComponentModel.DataAnnotations;

namespace Talabat.Dto
{
    public class ForgetPasswordDto
    {
        [Required]
        public string Email { get; set; }
    }
}
