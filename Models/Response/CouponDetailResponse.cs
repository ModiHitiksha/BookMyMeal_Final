using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace BookMyMeal.Models.Response
{
    public class CouponDetailResponse
    {
        [Required]
        public long CouponId { get; set; }

        [Required]
        public long MealBookingId { get; set; }

        [Required]
        public Boolean IsRedeem { get; set; }
        [Required]
        public string QRCodeUri { get; set; }
        [Required]
        public Boolean IsActive { get; set; }
    }
}
