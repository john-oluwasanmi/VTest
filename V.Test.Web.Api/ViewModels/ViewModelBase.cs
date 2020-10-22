using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using V.Test.Web.App.ViewModels.Interface;

namespace V.Test.Web.App.ViewModels
{
    public class ViewModelBase : IViewModel
    {
        public int Id { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        
    }
}
