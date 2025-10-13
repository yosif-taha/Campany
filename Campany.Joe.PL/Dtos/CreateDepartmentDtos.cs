using System.ComponentModel.DataAnnotations;

namespace Campany.Joe.PL.Dtos
{
    public class CreateDepartmentDtos
    {
        [Required (ErrorMessage ="Code Is Reqired")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name Is Reqired")]

        public string Name { get; set; }
        [Required(ErrorMessage = "CreateAt Is Reqired")]

        public DateTime CreateAt { get; set; }
    }
}
