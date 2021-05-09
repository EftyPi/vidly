using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MemberAgeValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // cast it to customer
            Customer customer = (Customer)validationContext.ObjectInstance;

            if (customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.PayAsYouGo)
            {
                // pay as you go
                return ValidationResult.Success;
            }

            if (customer.BirthDate == null)
            {
                return new ValidationResult("Birthdate is required");
            }

            int age = DateTime.Today.Year - customer.BirthDate.Value.Year;

            return (age > 18) 
                ? ValidationResult.Success 
                : new ValidationResult("Customer should be more than 18 years old to obtain a membership");

        }
    }
}