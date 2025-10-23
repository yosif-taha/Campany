using System.ComponentModel.DataAnnotations;

namespace Campany.Joe.PL.Dtos
{
    public class ForgerPasswordDto
    {
        [Required(ErrorMessage = "Email Must Is Reqired !!")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
