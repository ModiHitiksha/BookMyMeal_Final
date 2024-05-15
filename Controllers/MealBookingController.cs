using BookMyMeal.Data;
using BookMyMeal.Data.DataLayer;
using BookMyMeal.Models.Request;
using BookMyMeal.Models.Response;
using BookMyMeal.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookMyMeal.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MealBookingController : ControllerBase
    {

        private readonly IMealBookingService _mealBookingService;
        public MealBookingController(IMealBookingService mealBookingService)
        {
            _mealBookingService = mealBookingService;
        }

        [HttpGet("/GetMyMealBooking/{employeeLoginId}")]
        public ActionResult<List<MealBookingResponse>> GetMyMealBooking(long employeeLoginId)
        {
            /* Handles a GET request to "/api/mealbooking/GetMyMealBooking/{id}" to retrieve meal booking details for a specific
              employee.*/
            var myMealBooking = this._mealBookingService.GetMealBookingDetailsByEmployeeId(employeeLoginId);
            return Ok(myMealBooking);
        }

        [HttpPost("/BookMyMeal")]
        public ActionResult<List<MealBookingResponse>> BookMyMeal(MealBookingRequest mealBookingRequest)
        {
            // Handles a POST request to "/api/mealbooking/BookMyMeal" to book a meal for an employee.
            var myMealBooking = this._mealBookingService.BookMyMeal(mealBookingRequest);
            return Ok(myMealBooking);
        }

        [HttpGet("/CancelMyMealBooking/{mealBookingId}")]
        public ActionResult<List<MealBookingResponse>> CancelMyMealBooking(long mealBookingId)
        {
            // Handles a GET request to "/api/mealbooking/CancelMyMealBooking/{id}" to cancel a meal booking for an employee.
            var myMealBooking = this._mealBookingService.CancelBookMyMeal(mealBookingId);
            return Ok(myMealBooking);
        }
    }
}
