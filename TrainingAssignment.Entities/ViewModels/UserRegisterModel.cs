using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingAssignment.Entities.Models;

namespace TrainingAssignment.Entities.ViewModels
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "Please enter your First name.")]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false,ErrorMessage = "Please enter your Surname.")]
        public string SurName { get; set; }

       
        [Required(ErrorMessage = "Please enter your Phonenumber.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",ErrorMessage = "Entered phone format is not valid.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be strong.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%^&*()_+])[A-Za-z\d!@#$%^&*()_+]{8,100}$", ErrorMessage = "Password must be strong.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confrimpassword can't be empty.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage ="Please select the country")]
        public byte CountryId { get; set; }

        [Required(ErrorMessage = "Please select the state")]
        public byte StateId { get; set; }

        [Required(ErrorMessage = "Please select the city")]
        public byte CityId { get; set; }

        [Required(ErrorMessage ="Please select gender")]
        public byte GenderId { get; set; }
        public IFormFile? avatar { get; set; }
        public List<Country>? countries { get; set; }

    }
}
