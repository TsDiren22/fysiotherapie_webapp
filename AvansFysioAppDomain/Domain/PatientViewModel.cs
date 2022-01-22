using System;
using System.ComponentModel.DataAnnotations;
using AvansFysioAppDomain.Domain.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace AvansFysioAppDomain.Domain
{
    public class PatientViewModel
    {

        public int PatientId { get; set; }

        [Required(ErrorMessage = "Please enter your name!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your Email!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your phone number!")]
        [Phone]
        public string Phone { get; set; }
        
        public IFormFile Picture { get; set; }

        [Required(ErrorMessage = "Please choose an occupation")]
        public string Occupation { get; set; }

        public int OccupationId { get; set; }

        [Required(ErrorMessage = "Please enter your birthday!")]
        [AgeAnnotions(ErrorMessage = "The patient must to older than 16 years old!")]
        public DateTime? Birthday { get; set; }

        [Required(ErrorMessage = "Please enter your gender!")]
        public string Gender { get; set; }

        public IFormFile EditedPicture { get; set; }
        public int GetAge()
        {
            var today = DateTime.Now;

            var age = today.Year - Birthday.Value.Year;

            if (today.Month < Birthday.Value.Month || (today.Month == Birthday.Value.Month && today.Day < Birthday.Value.Day)) age--;
            return age;
        }
    }
}
