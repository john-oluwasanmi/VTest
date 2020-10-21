using V.Test.Web.App.Entities;

namespace V.Test.Web.App.ViewModels
{
    public partial class EmployeeViewModel : ViewModelBase
    {
        public int OrganisationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
