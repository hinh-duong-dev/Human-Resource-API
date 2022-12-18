using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Employee
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the First Name is 20 charaters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Last Name is 20 charaters.")]
        public string LastName { get; set; }

        public int Age { get; set; }


        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Address is 60 charaters.")]
        public string Address { get; set; }

        public string City { get; set; }

        public string Position { get; set; }

        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
