using BookMyMeal.Data.DataLayer;
using BookMyMeal.Models.Request;
using BookMyMeal.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace BookMyMeal.Repositories.Implementation
{
    public class EmployeeImplService : IEmployeeService
    {
        private readonly BookMyMealContext _bookedMyMealDbContext;

        public EmployeeImplService(BookMyMealContext bookedMyMealDbContext)
        {
            _bookedMyMealDbContext = bookedMyMealDbContext;
        }
        public Employeelogin? GetEmployeeDetailByEmail(string email)
        {
            try
            {
                var employeeDetails = this._bookedMyMealDbContext.Employeelogins.Where(emp => emp.Email == email).FirstOrDefault();
                return employeeDetails ?? null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            try
            {
                var data = this._bookedMyMealDbContext.Database.ExecuteSqlRaw($"EXEC Update_Password_EmployeeLogin_By_EmployeeId @empId = {changePasswordRequest.EmployeeLoginID}, @newPassword = '{changePasswordRequest.NewPassword}'");
                return "Password changed successfully.";
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public bool CreateNewEmployee(LoginRequest loginRequest)
        {
            try
            {
                var checkEmailExistOrNot = this._bookedMyMealDbContext.Employeelogins.Where(x => x.Email == loginRequest.Email).FirstOrDefault();

                if(checkEmailExistOrNot == null)
                {
                    this._bookedMyMealDbContext.Database.ExecuteSqlRaw($"EXEC Create_New_EmployeeLogin @emailId = '{loginRequest.Email}', @name = '{loginRequest.Name}', @password = '{loginRequest.Password}'");
                    return true;
                }                
            }
            catch (Exception)
            {
                throw;
            }           
            return false;
        }

    }
}
