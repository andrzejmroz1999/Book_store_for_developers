using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace book_store_for_developers.Models
{
    public class UserData
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        [RegularExpression(@"(\+\d{2})*[\d\s-]+",ErrorMessage = "Invalid phone number format")]
        public string PhoneNumber { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email adress format")]
        public string Email { get; set; }


    }
}