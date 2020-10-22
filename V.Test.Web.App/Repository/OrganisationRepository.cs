using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using V.Test.Web.App.Entities;
using V.Test.Web.App.Repository.Interface;

namespace V.Test.Web.App.Repository
{
    public   class OrganisationRepository : RepositoryBase<Organisation>,
        IOrganisationRepository
    {
        public OrganisationRepository(IConfiguration configuration,  ILogger<Organisation> logger)
              : base(configuration, logger)
        {
        }
        public override async Task<Organisation> GetAsync(int id)
        {
            try
            {
                using (var context = new VTestsContext(OptionsBuilder.Options))
                {
                    var entity = await context.Set<Organisation>()
                                             .Where(x => x.Id == id && (x.IsDeleted == null || x.IsDeleted == false))
                                             .Include(r => r.Address)
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

        public override async Task<List<Organisation>> ListAsync(int pageNumber)
        {
            try
            {
                CurrentPageNumber = pageNumber;

                using (var context = new VTestsContext(OptionsBuilder.Options))
                {
                    List<Organisation> result = await context.Set<Organisation>().AsNoTracking()
                                                .Where(e => e.IsDeleted == false || e.IsDeleted == null)
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
