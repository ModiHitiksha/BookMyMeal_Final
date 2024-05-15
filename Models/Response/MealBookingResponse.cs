namespace BookMyMeal.Models.Response
{
    public class MealBookingResponse
    {
        public long MealBookingId { get; set; }
        public long EmployeeLoginId { get; set; }
        public string MealType {  get; set; }
        public DateTime BookingDate { get; set; }
        public Boolean IsBooked { get; set; }
        public CouponDetailResponse couponDetailResponse { get; set; }
        public string BookingStatus => this.IsBooked ? "Booked" : "Cancelled";

    }
}
