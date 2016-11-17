using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        
        // se necesita esta propiedad para acceder a la información del MemberShip correspondiente -> Eager Loading !!
        public MembershipType MemberShipType { get; set; }
        public byte MembershipTypeId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name="Date of birth")]
        public DateTime? Birthdate { get; set; }
    }
}