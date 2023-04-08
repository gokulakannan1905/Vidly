using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Dtos;

namespace Vidly.Models
{
    public class Min18YearsOld : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = validationContext.ObjectInstance as Customer;
            if(customer.MembershipTypeId == MembershipType.Unknown || 
                customer.MembershipTypeId == MembershipType.PayAsYouGo) //avoid magic numbers 
            {
                return ValidationResult.Success;
            }
            if(customer.Birthday == null)
            {
                return new ValidationResult("Birthday is required");
            }

            var age = DateTime.Now.Year - customer.Birthday.Value.Year;
            return (age >= 18) ? ValidationResult.Success : new ValidationResult("Customer should be atleast 18 years old to go on a membership");
        }
    }
}