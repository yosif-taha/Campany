using System.ComponentModel.DataAnnotations;

namespace Campany.Joe.PL.Dtos
{
    public class SignUpDto
    {
        [Required(ErrorMessage ="User Name Must Is Reqired !!")]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email Must Is Reqired !!")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Must Is Reqired !!")]
        [DataType(DataType.Password)]   //*********
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",ErrorMessage ="Invalid password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword Must Is Reqired !!")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage = "ConfirmPassword Must Match With Password")]
        public string ConfirmPasseword { get; set; }
        [Required(ErrorMessage = "Is Agree Must Is Reqired !!")] 
        public  bool IsAgree { get; set; }
    }
}
