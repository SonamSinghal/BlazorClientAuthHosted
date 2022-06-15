using BlazorClientAuthHosted.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorClientAuthHosted.Shared
{
    public class UserModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Phone Number")]
        public long? Phone { get; set; }

        public int? Age { get; set; }

        public Gender Gender { get; set; }

        [Required, DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime Dob { get; set; }

        public string Email { get; set; }
    }
}
