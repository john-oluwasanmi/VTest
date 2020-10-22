using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using V.Test.Web.App.Entities;

namespace V.Test.Web.App.ViewModels
{
    public partial class AddressViewModel : ViewModelBase
    {
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
