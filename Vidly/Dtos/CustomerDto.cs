using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; } //PK

        [Required]
        public string Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        public byte MembershipTypeId { get; set; } //FK

        public MembershipTypeDto MembershipType { get; set; }

        //[Min18YearsOld]
        public DateTime? Birthday { get; set; }
    }
}