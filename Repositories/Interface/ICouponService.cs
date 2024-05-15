using BookMyMeal.Models.Response;

namespace BookMyMeal.Repositories.Interface
{
    public interface ICouponService
    {
        CouponDetailResponse GetCouponById(long couponId);
        CouponDetailResponse GetCouponMealBookingId(long mealBookingId);
    }
}
