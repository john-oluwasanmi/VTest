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
            services.AddScoped<IAddressBusinessService, AddressBusinessService>();
            services.AddScoped<IEmployeeBusinessService, EmployeeBusinessService>();
            services.AddScoped<IOrganisationBusinessService, OrganisationBusinessService>();
        }

        public static void AddRepository(IServiceCollection services)
        {
            services.AddScoped<IOrganisationRepository, OrganisationRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<AddressRepository, AddressRepository>();
        }
    }
}
