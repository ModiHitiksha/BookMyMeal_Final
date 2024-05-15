using System.ComponentModel.DataAnnotations;

namespace BookMyMeal.Models.Request
{
    public class ChangePasswordRequest
    {
        [Required]
        public long EmployeeLoginID {  get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set;
        }

    }
}
