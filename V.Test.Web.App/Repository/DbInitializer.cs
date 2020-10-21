using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace V.Test.Web.App.Repository
{

    public static class DbInitializer
    {
        public static void Initialize(VTestsContext context)
        {
            context.Database.EnsureCreated();

            if (context.Addresses.Any())
            {
                return;    
            }


            if (context.Organisations.Any())
            {
                return;
            }

            if (context.Employees.Any())
            {
                return;
            }
        }
    }

}
