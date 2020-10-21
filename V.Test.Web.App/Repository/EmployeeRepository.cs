using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using V.Test.Web.App.Entities;
using V.Test.Web.App.Repository.Interface;

namespace V.Test.Web.App.Repository
{
    public partial class EmployeeRepository : RepositoryBase<Employee>,
        IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration, ILogger<Employee> logger)
              : base(configuration, logger)
        {
        }
    }
}
