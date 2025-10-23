using System.ComponentModel.DataAnnotations;

namespace Campany.Joe.PL.Dtos
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Password Must Is Reqired !!")]
        [DataType(DataType.Password)]   //*********
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Invalid password")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "ConfirmPassword Must Is Reqired !!")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "ConfirmPassword Must Match With Password")]
        public string ConfirmPasseword { get; set; }
    }
}
