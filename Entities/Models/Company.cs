using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Company
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Name is 20 charaters.")]
        public string Name { get; set; }

        public int Age { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Address is 60 charaters.")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Country is required.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Country is 60 charaters.")]
        public string Country { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
