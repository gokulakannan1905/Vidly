using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; } //PK

        [Required]
        public string Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        [Display(Name = "Membership Type")]
        public MembershipType MembershipType { get; set; }

        public byte MembershipTypeId { get; set; } //FK

        [Display(Name = "Date Of Birth")]
        [Min18YearsOld]
        public DateTime? Birthday { get; set; }
    }
}