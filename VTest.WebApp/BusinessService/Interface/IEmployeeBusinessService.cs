using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using V.Test.Web.App.Entities;

namespace V.Test.Web.App.BusinessService.Interface
{
    public interface IEmployeeBusinessService : IBusinessService<Employee>
    {
        Task<List<Employee>> ListByOrganisationAsync(int organisationId, int pageNumber);
    }
}
