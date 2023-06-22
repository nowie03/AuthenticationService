using System.Security.Policy;

namespace AuthenticationService.Models
{
    public class LoginBody
    {
        public string email { get; set; }
        public string password { get; set; }

        public LoginBody(String email,String password)
        {
            this.email = email;
            this.password = password;
        }
    }
}
