using System.ComponentModel.DataAnnotations;

namespace BookMyMeal.Models.Request
{
    public class MealBookingRequest
    {
        [Required]
        public long EmployeeLoginID { get; set; }

        [Required]
        public string MealType { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }
    }
}
