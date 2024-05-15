namespace BookMyMeal.Models.Response
{
    public class LoginResponse
    {
        public long EmployeeLoginID { get; set; }
        public string Emaill {  get; set; }
        public string JWTToken { get; set; }
        public string Name { get; set; }
    }
}
