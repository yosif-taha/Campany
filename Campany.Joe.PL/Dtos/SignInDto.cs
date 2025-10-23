using System.ComponentModel.DataAnnotations;

namespace Campany.Joe.PL.Dtos
{
    public class SignInDto
    {
        [Required(ErrorMessage = "Email Must Is Reqired !!")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Must Is Reqired !!")]
        [DataType(DataType.Password)]   //*********
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Invalid password")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
