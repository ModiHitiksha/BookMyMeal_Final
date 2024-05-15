using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookMyMeal.Data.DataLayer
{
    [Table("bookmymeal")]
    public partial class Bookmymeal
    {
        public Bookmymeal()
        {
            Coupons = new HashSet<Coupon>();
        }

        [Key]
        [Column("meal_booking_id")]
        public long MealBookingId { get; set; }
        [Column("employee_login_id")]
        public long EmployeeLoginId { get; set; }
        [Column("meal_type")]
        [StringLength(20)]
        [Unicode(false)]
        public string MealType { get; set; } = null!;
        [Column("booking_date", TypeName = "date")]
        public DateTime BookingDate { get; set; }
        [Required]
        [Column("is_booked")]
        public bool? IsBooked { get; set; }
        [Column("created_by")]
        public long CreatedBy { get; set; }
        [Column("created_on", TypeName = "date")]
        public DateTime CreatedOn { get; set; }
        [Column("updated_on", TypeName = "date")]
        public DateTime? UpdatedOn { get; set; }

        [ForeignKey("CreatedBy")]
        [InverseProperty("BookmymealCreatedByNavigations")]
        public virtual Employeelogin CreatedByNavigation { get; set; } = null!;
        [ForeignKey("EmployeeLoginId")]
        [InverseProperty("BookmymealEmployeeLogins")]
        public virtual Employeelogin EmployeeLogin { get; set; } = null!;
        [InverseProperty("MealBooking")]
        public virtual ICollection<Coupon> Coupons { get; set; }
    }
}
