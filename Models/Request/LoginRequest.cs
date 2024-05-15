using System.ComponentModel.DataAnnotations;

namespace BookMyMeal.Models.Request
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email Address is required.")]
        [EmailAddress(ErrorMessage = "Email Address should be valid.")]
        [StringLength(100, ErrorMessage = "Email Address should allow 100 character")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(255)]
        public string Password { get; set; }
        
      // [Required]
        [StringLength(100)]
        public string? Name { get; set; }

    }
}
