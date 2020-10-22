using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using V.Test.Web.Api.Entities;
using V.Test.Web.Api.Repository.Interface;

namespace V.Test.Web.Api.Repository
{
    public   class AddressRepository : RepositoryBase<Address>,
        IAddressRepository
    {
        public AddressRepository(IConfiguration configuration, ILogger<Address> logger)
              : base(configuration, logger)
        {
        }

       

    }
}
