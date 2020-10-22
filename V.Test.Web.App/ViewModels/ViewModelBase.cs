using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using V.Test.Web.App.ViewModels.Interface;

namespace V.Test.Web.App.ViewModels
{
    public class ViewModelBase : IViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedOn { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedOn { get; set; }
        
    }
}
