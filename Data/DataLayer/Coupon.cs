using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookMyMeal.Data.DataLayer
{
    [Table("coupon")]
    public partial class Coupon
    {
        [Key]
        [Column("coupon_id")]
        public long CouponId { get; set; }
        [Column("meal_booking_id")]
        public long MealBookingId { get; set; }
        [Column("qrcode_uri")]
        public string QrcodeUri { get; set; } = null!;
        [Required]
        [Column("is_active")]
        public bool? IsActive { get; set; }
        [Column("is_redeem")]
        public bool IsRedeem { get; set; }
        [Column("created_on", TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        [Column("update_on", TypeName = "datetime")]
        public DateTime? UpdateOn { get; set; }

        [ForeignKey("MealBookingId")]
        [InverseProperty("Coupons")]
        public virtual Bookmymeal MealBooking { get; set; } = null!;
    }
}
