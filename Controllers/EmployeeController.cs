using BookMyMeal.Models.Request;
using BookMyMeal.Models.Response;
using BookMyMeal.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookMyMeal.Controllers
{
    //[Authorize] bhai ne puchvanu che
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }        

        [HttpPost("/change-password")]
        public ActionResult<ValidateResponse> ChangeEmployeePassword(ChangePasswordRequest changePasswordRequest)
        {
            ValidateResponse validateResponse = new ValidateResponse();
            try
            {
                // Handles a POST request to "/api/employee/change-password" for changing employee passwords.
                // Validates employee ID, current password, new password, and confirm password before proceeding with the password change.
                if (changePasswordRequest.EmployeeLoginID <= 0)
                {
                    validateResponse.Message = "Please try again later.";
                    return Ok(validateResponse);
                }
                if (changePasswordRequest.CurrentPassword == "")
                {
                    validateResponse.Message = "Current Password should not be empty.";
                    return Ok(validateResponse);
                }
                if (changePasswordRequest.NewPassword == "")
                {
                    validateResponse.Message = "New Password should not be empty.";
                    return Ok(validateResponse);
                }
                if (changePasswordRequest.ConfirmPassword == "")
                {
                    validateResponse.Message = "Confirm Password should not be empty.";
                    return Ok(validateResponse);
                }
                if (!changePasswordRequest.NewPassword.Equals(changePasswordRequest.ConfirmPassword))
                {
                    validateResponse.Message = "New Password and Confirm Password should be same.";
                    return Ok(validateResponse);
                }
                // Calls the `ChangePassword` method in `EmployeeImplService` to update the password.
                var employeeDetails = this._employeeService.ChangePassword(changePasswordRequest);
                validateResponse.Message = employeeDetails;

                // Checks if the password change was successful and returns the updated employee details if successful.
                if (employeeDetails != null)
                {
                    return Ok(validateResponse);
                }
            }
            catch
            {
                throw;
            }
            return Ok(validateResponse);
        }
    }
}
