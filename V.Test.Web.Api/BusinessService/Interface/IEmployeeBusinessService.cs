using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using V.Test.Web.Api.Entities;

namespace V.Test.Web.Api.BusinessService.Interface
{
    public interface IEmployeeBusinessService : IBusinessService<Employee>
    {
        Task<List<Employee>> ListByOrganisationAsync(int organisationId, int pageNumber);
    }
}
