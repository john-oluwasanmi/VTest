using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using V.Test.Web.App.BusinessService.Interface;
using V.Test.Web.App.Entities;
using V.Test.Web.App.Repository.Interface;

namespace V.Test.Web.App.BusinessService
{
    public   class EmployeeBusinessService : BusinessServiceBase<Employee, IEmployeeRepository>
        , IEmployeeBusinessService
    {
        public EmployeeBusinessService(IEmployeeRepository   employeeRepository)
           : base(employeeRepository)
        { }

        public async Task<List<Employee>> ListByOrganisationAsync(int organisationId, int pageNumber)
        {
            ValidateId(organisationId);
            ValidateId(pageNumber);

            var entities = await RepositoryManager.ListByOrganisationAsync(organisationId, pageNumber);
            return entities;
        }
    }
}
