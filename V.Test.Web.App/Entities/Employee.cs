using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace V.Test.Web.App.Entities
{
    public   class Employee : Entity
    {
      
        public int OrganisationId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
