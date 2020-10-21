using System.Collections.Generic;
using V.Test.Web.App.Entities;

namespace V.Test.Web.App.ViewModels
{
    public partial class AddressViewModel : ViewModelBase
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string Town { get; set; }
        public string Postcode { get; set; }

        public virtual ICollection<Organisation> Organisation { get; set; }
    }
}
