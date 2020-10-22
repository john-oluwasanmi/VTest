using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace V.Test.Web.App.Entities.Interface
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime? ModifiedOn { get; set; }
        bool? IsDeleted { get; set; }
    }
}
