using System;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        // to override the error message displayed in the input of the form
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }

        public MembershipTypeDto MembershipType { get; set; }
        public byte MembershipTypeId { get; set; }

        // [MemberAgeValidation]
        public DateTime? BirthDate { get; set; }
    }
}