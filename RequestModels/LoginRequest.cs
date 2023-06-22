using System.Security.Policy;

namespace AuthenticationService.RequestModels
{
    public class LoginBody
    {
        public string email { get; set; }
        public string password { get; set; }

        public LoginBody(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }
}
