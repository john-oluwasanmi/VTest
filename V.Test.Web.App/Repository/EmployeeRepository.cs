using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Employee>> ListByOrganisationAsync(int organisationId, int pageNumber)
        {
            try
            {
                CurrentPageNumber = pageNumber;

                using (var context = new VTestsContext(OptionsBuilder.Options))
                {
                    List<Employee> result = await context.Set<Employee>().AsNoTracking()
                                                .Where(e => (e.IsDeleted == false || e.IsDeleted == null)
                                                        && e.OrganisationId == organisationId)
                                                .OrderBy(r => r.Id)
                                                .Skip(SkippedDbRecordSize)
                                                .Take(MaxPageSize)
                                                .ToListAsync();

                    var typeName = Entity?.GetType()?.Name;
                    VLogger.LogInformation($" Successfully retrieved {typeName}'s List with page number {pageNumber} ");
                    CurrentPageNumber = 0;

                    return result;
                }
            }
            catch (SqlNullValueException s)
            {
                LogError(s, null, pageNumber);

                throw s;
            }
            catch (Exception ex)
            {
                LogError(ex, null, pageNumber);

                throw ex;
            }

        }
    }
}
