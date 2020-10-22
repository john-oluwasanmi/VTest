using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using V.Test.Web.Api.Entities;
using V.Test.Web.Api.Repository.Interface;

namespace V.Test.Web.Api.Repository
{
    public   class EmployeeRepository : RepositoryBase<Employee>,
        IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration, ILogger<Employee> logger)
              : base(configuration, logger)
        {
        }

        public override async Task<Employee> GetAsync(int id)
        {
            try
            {
                using (var context = new VTestsContext(OptionsBuilder.Options))
                {
                    var entity = await context.Set<Employee>()
                                             .Where(x => x.Id == id && (x.IsDeleted == null || x.IsDeleted == false))
                                             .Include(r => r.Organisation)
                                            .SingleOrDefaultAsync();

                    var typeName = Entity?.GetType()?.Name;
                    VLogger.LogInformation($" Successfully retrieved {typeName} with the Id: '{entity?.Id} ");

                    return entity;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, null, id);

                throw ex;
            }
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
                                                        .Include(r=>r.Organisation)
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
