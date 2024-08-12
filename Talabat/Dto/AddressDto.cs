using System.ComponentModel.DataAnnotations;

namespace Talabat.Dto
{
    public class AddressDto
    {
   
        [Required]
        public string Country { get; set; }
        [Required]

        public string City { get; set; }
        [Required]
        public string Street { get; set; }

    }
}
