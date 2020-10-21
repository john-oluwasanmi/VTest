using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using V.Test.Web.App.Entities;

namespace V.Test.Web.App.Repository
{

    public static class DbInitializer
    {
        public static void Initialize(VTestsContext context)
        {
            context.Database.EnsureCreated();

            if (context.Addresses.Any())
            {
                var addresses = new List<Address>
               {
                 new Address{ AddressLine1 ="1 Churchill Place", AddressLine2="", AddressLine3="",AddressLine4="", Town ="London", Postcode="E14 5HP" }
                ,new Address{ AddressLine1 ="8 Canada Square", AddressLine2="", AddressLine3="",AddressLine4="", Town ="London", Postcode="E14 5HQ" }
                ,new Address{ AddressLine1 ="25 Gresham Street", AddressLine2="", AddressLine3="",AddressLine4="", Town ="London", Postcode="EC2V 7HN" }
                ,new Address{ AddressLine1 ="Henry Duncan House", AddressLine2="120 George Street", AddressLine3="",AddressLine4="", Town ="Edinburgh", Postcode="EH2 4LH" }
                ,new Address{ AddressLine1 ="30 St Vincent Place", AddressLine2="", AddressLine3="",AddressLine4="", Town ="Glasgow", Postcode="EC2V 5DD" }
                ,new Address{ AddressLine1 ="1 Basinghall Avenue", AddressLine2="", AddressLine3="",AddressLine4="", Town ="London", Postcode="G1 2HL" }
                ,new Address{ AddressLine1 ="The Mound", AddressLine2="", AddressLine3="",AddressLine4="", Town ="Edinburgh", Postcode="EH1 1YZ" }
              };

                context.Addresses.AddRange(addresses);
                context.SaveChanges();
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
