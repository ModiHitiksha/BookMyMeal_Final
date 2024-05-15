using BookMyMeal.Data;
using BookMyMeal.Data.DataLayer;
using BookMyMeal.Models.Request;

namespace BookMyMeal.Repositories.Interface
{
    public interface IEmployeeService
    {
        Employeelogin? GetEmployeeDetailByEmail(String email);  // Get employee by email

        Boolean CreateNewEmployee(LoginRequest loginRequest);      // Creates a new employee account

        String ChangePassword(ChangePasswordRequest changePasswordRequest);   // Change an employee's password

    }
}
