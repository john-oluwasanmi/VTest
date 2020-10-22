using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace V.Test.Web.Api.Entities
{
    public   class Address : Entity
    {
        public Address()
        {
            Organisation = new HashSet<Organisation>();
        }

        [Required]
      
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }

        [Required]
        public string Town { get; set; }

        [Required]
        public string Postcode { get; set; }

        public virtual ICollection<Organisation> Organisation { get; set; }
    }
}
