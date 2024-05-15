using BookMyMeal.Models.Request;
using BookMyMeal.Models.Response;

namespace BookMyMeal.Repositories.Interface
{
    public interface IMealBookingService
    {
        List<MealBookingResponse> GetMealBookingDetailsByEmployeeId(long employeeLoginId);  // Get meal booking details by employee login ID

        List<MealBookingResponse> BookMyMeal(MealBookingRequest mealBookingRequest);   // Book meals for a specific employee

        List<MealBookingResponse> CancelBookMyMeal(long mealBookingDetailsId);   // Cancel a meal booking for an employee
    }
}
