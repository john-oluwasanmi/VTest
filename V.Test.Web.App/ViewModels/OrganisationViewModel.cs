using System.Collections.Generic;
using V.Test.Web.App.Entities;

namespace V.Test.Web.App.ViewModels
{
    public partial class OrganisationViewModel : ViewModelBase
    {
        

        public int AddressId { get; set; }
        public string OrganisationName { get; set; }
        public string OrganisationNumber { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
