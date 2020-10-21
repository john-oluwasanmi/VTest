using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using V.Test.Web.Api.Entities;

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
