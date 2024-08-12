namespace Talabat.Domain.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public string AppUserId { get; set; } // As A forign Key 
        public AppUser User { get; set; } //Navigational Properity [one]
    }
}