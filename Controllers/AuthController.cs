using BookMyMeal.Data;
using BookMyMeal.Data.DataLayer;
using BookMyMeal.Models.Request;
using BookMyMeal.Models.Response;
using BookMyMeal.Repositories.Implementation;
using BookMyMeal.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BookMyMeal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _config;
        private IEmployeeService _employeeService;
        public AuthController(IConfiguration config, IEmployeeService employeeService)
        {
            _config = config;
            _employeeService = employeeService;
        }

        [HttpPost("/login")]
        public ActionResult<LoginResponse> EmployeeLogin([FromBody] LoginRequest loginRequest)
        {
            LoginResponse loginResponse = new LoginResponse();
            if (loginRequest != null)
            {
                var employeeDetails = this._employeeService.GetEmployeeDetailByEmail(loginRequest.Email);
                if(employeeDetails != null && employeeDetails.Password == loginRequest.Password)
                {                    
                    loginResponse.Emaill = employeeDetails.Email;
                    loginResponse.Name = employeeDetails.Name;
                    loginResponse.EmployeeLoginID = employeeDetails.EmpId;
                    loginResponse.JWTToken = this.GenerateJWTToken();
                }
            }
            
            return Ok(loginResponse);
        }

        [HttpPost("/Create")]
        public ActionResult<bool> CreateEmployeeLogin(LoginRequest loginRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Handles a POST request to "/api/employee/Create" to create a new employee with provided login credentials.
                    var isUserCreated = this._employeeService.CreateNewEmployee(loginRequest);

                    // Calls the `CreateNewEmployee` method in `EmployeeImplService` to create a new employee.
                    if (isUserCreated)
                    {
                        return Ok(true);
                    }
                }

                return Ok(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GenerateJWTToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenExpiredInMinutes = Convert.ToDouble(_config["Jwt:ExpiredInMinutes"]);

            var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(tokenExpiredInMinutes),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
            return token;
        }
    }
}
