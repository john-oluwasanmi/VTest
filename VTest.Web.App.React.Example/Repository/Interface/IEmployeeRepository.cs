using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using V.Test.Web.Api.Entities;

namespace V.Test.Web.Api.Repository.Interface
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<List<Employee>> ListByOrganisationAsync(int organisationId, int pageNumber);
    }
}
