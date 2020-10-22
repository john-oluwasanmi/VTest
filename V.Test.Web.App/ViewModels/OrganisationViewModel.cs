using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using V.Test.Web.App.Entities;

namespace V.Test.Web.App.ViewModels
{
    public partial class OrganisationViewModel : ViewModelBase
    {

        [Required]
        public int? AddressId { get; set; }

        [Required]
        [Display(Name ="Name")]
        public string OrganisationName { get; set; }

        [Required]
        [Display(Name = "Number")]
        public string OrganisationNumber { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
