using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.Models
{
    public class UserAddress
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public String Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

    }
}
