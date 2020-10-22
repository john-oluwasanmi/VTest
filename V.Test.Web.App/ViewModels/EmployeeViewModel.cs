using System.ComponentModel.DataAnnotations;
using V.Test.Web.App.Entities;

namespace V.Test.Web.App.ViewModels
{
    public partial class EmployeeViewModel : ViewModelBase
    {
        [Required]
        [Display(Name = "Organisation")]
        public int OrganisationId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
