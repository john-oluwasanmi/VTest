using System.ComponentModel.DataAnnotations;
using V.Test.Web.App.Entities;

namespace V.Test.Web.App.ViewModels
{
    public partial class EmployeeViewModel : ViewModelBase
    {
        [Required]
        public int OrganisationId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
