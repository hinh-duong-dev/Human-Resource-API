using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class CompanyManipulationDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 20 charaters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(250, ErrorMessage = "Maximum length for the Address is 60 charaters.")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Country is required.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Country is 60 charaters.")]
        public string Country { get; set; }
    }
}
