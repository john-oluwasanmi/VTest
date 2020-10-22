using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
    }
}
