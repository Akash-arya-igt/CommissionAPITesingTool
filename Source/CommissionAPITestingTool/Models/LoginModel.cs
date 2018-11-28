using System.ComponentModel;

namespace CommissionAPITestingTool.Models
{
    public class LoginModel
    {
        [DisplayName("User ID")]
        public string UserId { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }
    }
}