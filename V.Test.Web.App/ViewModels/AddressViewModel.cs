using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using V.Test.Web.App.Entities;

namespace V.Test.Web.App.ViewModels
{
    public partial class AddressViewModel : ViewModelBase
    {
        [Required]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Display(Name = "Address Line 3")]
        public string AddressLine3 { get; set; }

        [Display(Name = "Address Line 4")]
        public string AddressLine4 { get; set; }

        [Required]
        public string Town { get; set; }

        [Required]
        [Display(Name = "Post Code")]
        public string Postcode { get; set; }

        public virtual ICollection<Organisation> Organisation { get; set; }
    }
}
