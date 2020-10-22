using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using V.Test.Web.App.BusinessService;
using V.Test.Web.App.BusinessService.Interface;
using V.Test.Web.App.Repository;
using V.Test.Web.App.Repository.Interface;

namespace V.Test.Web.App.Core
{
    public static class Container
    {
        public static void AddService(IServiceCollection services)
        {
            services.AddSingleton<IAddressBusinessService, AddressBusinessService>();
            services.AddSingleton<IEmployeeBusinessService, EmployeeBusinessService>();
            services.AddSingleton<IOrganisationBusinessService, OrganisationBusinessService>();
        }

        public static void AddRepository(IServiceCollection services)
        {
            services.AddSingleton<IOrganisationRepository, OrganisationRepository>();
            services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
            services.AddSingleton<IAddressRepository, AddressRepository>();
        }
    }
}
