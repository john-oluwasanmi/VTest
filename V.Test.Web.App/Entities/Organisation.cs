using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace V.Test.Web.App.Entities
{
    public   class Organisation : Entity
    {
        public Organisation()
        {
            Employee = new HashSet<Employee>();
        }

  
        public int? AddressId { get; set; }

        [Required]
        public string OrganisationName { get; set; }

        [Required]
        public string OrganisationNumber { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
