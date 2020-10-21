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

            if (!context.Address.Any())
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

                context.Address.AddRange(addresses);
                context.SaveChanges();
            }


            if (!context.Organisation.Any())
            {
                var organisation = new List<Organisation>
               {
                 new Organisation{OrganisationName="Barclays UK PLC", OrganisationNumber="09740322", AddressId=  1}
                ,new Organisation{OrganisationName="HSBC BANK PLC", OrganisationNumber="00014259", AddressId=2  }
                ,new Organisation{OrganisationName="LLOYDS BANK PLC", OrganisationNumber="00002065", AddressId= 3 }
                ,new Organisation{OrganisationName="TSB BANK PLC", OrganisationNumber="SC095237", AddressId=4  }
                ,new Organisation{OrganisationName="CLYDESDALE BANK PLC", OrganisationNumber="SC001111", AddressId=5 }
                ,new Organisation{OrganisationName="STANDARD CHARTERED PLC", OrganisationNumber="00966425", AddressId= 6 }
                ,new Organisation{OrganisationName="BANK OF SCOTLAND PLC", OrganisationNumber="SC327000", AddressId=7  }
              };

                context.Organisation.AddRange(organisation);
                context.SaveChanges();
            }

            if (context.Employee.Any())
            {
                var employee = new List<Employee>
               {
                new Employee { OrganisationId = 1, FirstName = "Janet", LastName = "Smith" },
                  new Employee { OrganisationId = 1,    FirstName="Frank",LastName = "Bloswick"},
                 new Employee { OrganisationId = 1,    FirstName= "Tonya",LastName = "Bazinaw"},
                new Employee { OrganisationId = 1,    FirstName= "Mike",LastName = "St. Onge"},
                new Employee { OrganisationId = 1,    FirstName= "Jackie",LastName = "Jones"},
                new Employee { OrganisationId = 1,    FirstName= "Darren",LastName = "Tillbrooke"},
                new Employee { OrganisationId = 1,    FirstName= "Stephanie",LastName = "Holsinger"},
                new Employee { OrganisationId = 1,    FirstName= "Rene",LastName = "Hughey"},
                new Employee { OrganisationId = 1,    FirstName= "Robert",LastName = "Rogers"},
                new Employee { OrganisationId = 1,    FirstName= "Richard",LastName = "LaPine"},
                new Employee { OrganisationId = 1,    FirstName= "Kathy",LastName = "Summerfield"},
                new Employee{OrganisationId = 1,    FirstName = "Kathy",LastName = "Bodwin"},


                new Employee{OrganisationId = 3, FirstName = "Mitch",LastName = "Krause" },
                new Employee{OrganisationId = 3, FirstName = "George",LastName = "Dow" },
                new Employee{OrganisationId = 3, FirstName = "Jack",LastName = "Malone" },
                new Employee{OrganisationId = 3, FirstName = "Bill",LastName = "Schweiz" },
                new Employee{OrganisationId = 3, FirstName = "Mark",LastName = "Gunter" },
                new Employee{OrganisationId = 3, FirstName = "Bob",LastName = "Anderson" },
                new Employee{OrganisationId = 3, FirstName = "Scott",LastName = "Simpson" },
                new Employee{OrganisationId = 3, FirstName = "Phil",LastName = "ingman" },
                new Employee{OrganisationId = 3, FirstName = "Chad",LastName = "Leiker" },
                new Employee{OrganisationId = 3, FirstName = "Ian",LastName = "Benson" },
                new Employee{OrganisationId = 3, FirstName = "Nicole",LastName = "Lane" },
                new Employee{OrganisationId = 3, FirstName = "Steve",LastName = "Lundeen" },
                new Employee{OrganisationId = 3, FirstName = "Erica",LastName = "Black" },
                new Employee{OrganisationId = 3, FirstName = "Xenos",LastName = "Mathis" },
                new Employee{OrganisationId = 3, FirstName = "Kyle",LastName = "Good" },
                new Employee{OrganisationId = 3, FirstName = "Raja",LastName = "Dejesus" },
                new Employee{OrganisationId = 3, FirstName = "Timothy",LastName = "Frazier" },
                new Employee{OrganisationId = 3, FirstName = "Francine",LastName = "Morrison" },

                new Employee{OrganisationId = 4,FirstName = "Avram",    LastName = "Pate" },
                new Employee{OrganisationId = 4,FirstName = "Hammett",    LastName = "Coffey" },
                new Employee{OrganisationId = 4,FirstName = "Hasad",    LastName = "Wise" },
                new Employee{OrganisationId = 4,FirstName = "Cullen",    LastName = "Riddle" },
                new Employee{OrganisationId = 4,FirstName = "Kato",    LastName = "Delgado" },
                new Employee{OrganisationId = 4,FirstName = "Todd",    LastName = "Wright" },
                new Employee{OrganisationId = 4,FirstName = "Troy",    LastName = "Mccoy" },
                new Employee{OrganisationId = 4,FirstName = "Gil",    LastName = "Duncan" },
                new Employee{OrganisationId = 4,FirstName = "Lionel",    LastName = "Espinoza" },
                new Employee{ OrganisationId = 4,    FirstName = "Victor",    LastName = "Merrill" },
                new Employee{OrganisationId = 5,FirstName = "Gennifer",     LastName = "Vance" },
                new Employee{OrganisationId = 5,FirstName = "Chancellor",     LastName = "Warner" },
                new Employee { OrganisationId = 5,     FirstName = "Davis",     LastName = "Wolf" },
                new Employee{OrganisationId = 6,FirstName = "Carlos",         LastName = "Clarke" },
                new Employee{OrganisationId = 6,FirstName = "Dolan",         LastName = "Mercado" },
                new Employee{OrganisationId = 6,FirstName = "Helen",         LastName = "Guthrie" },
                new Employee{OrganisationId = 6,FirstName = "Elmo",         LastName = "Douglas" },
                new Employee{OrganisationId = 6,FirstName = "Kane",         LastName = "Rice" },
                new Employee{OrganisationId = 6,FirstName = "Colt",         LastName = "Rowland" },
                new Employee{OrganisationId = 6,FirstName = "John",         LastName = "Rose" },
                new Employee{OrganisationId = 6,FirstName = "Alfonso",         LastName = "Hopkins" },
                new Employee{OrganisationId = 6,FirstName = "Ida",         LastName = "Watts" },
                new Employee{OrganisationId = 6,FirstName = "Jennifer",         LastName = "Coleman" },
                new Employee{OrganisationId = 6,FirstName = "Ciaran",         LastName = "Newton" },
                new Employee{OrganisationId = 6,FirstName = "Hiram",         LastName = "Carrillo" },
                new Employee{OrganisationId = 6,FirstName = "Devin",         LastName = "Russell" },
                new Employee{OrganisationId = 6,FirstName = "Arsenio",         LastName = "Jensen" },
                new Employee{OrganisationId = 6,FirstName = "Otto",         LastName = "Gibbs" },
                new Employee {OrganisationId = 6, FirstName = "Hiram",         LastName = "Vega" },
                new Employee {  OrganisationId = 7,  FirstName = "Jarrod",         LastName = "Randolph" },
                new Employee {  OrganisationId = 7,  FirstName = "Josiah",         LastName = "Gates" },
                new Employee {  OrganisationId = 7,  FirstName = "Brandon",         LastName = "Stanley" },
                new Employee {  OrganisationId = 7,  FirstName = "Kennedy",         LastName = "Nunez" },
                new Employee {  OrganisationId = 7,  FirstName = "Lewis",         LastName = "Sanchez" },
                new Employee {  OrganisationId = 7,  FirstName = "Kassie",         LastName = "Chaney" },
                new Employee {  OrganisationId = 7,  FirstName = "Lance",         LastName = "Knox" },
                new Employee {  OrganisationId = 7,  FirstName = "Lamar",         LastName = "Harrison" },
                new Employee {  OrganisationId = 7,  FirstName = "Honorato",         LastName = "Montgomery" },
                new Employee {  OrganisationId = 2,  FirstName = "Lisa",         LastName = "Nielsen" },
                new Employee {  OrganisationId = 2,  FirstName = "Layla",         LastName = "Barr" },
                new Employee {  OrganisationId = 2,  FirstName = "Nancy",         LastName = "Mcclain" },
                new Employee {  OrganisationId = 2,  FirstName = "Kato",         LastName = "Delgado" },
                new Employee {  OrganisationId = 2,  FirstName = "Todd",         LastName = "Wright" },
                new Employee {  OrganisationId = 2,  FirstName = "Troy",         LastName = "Mccoy" },
                new Employee {  OrganisationId = 2,  FirstName = "Gil",         LastName = "Duncan" },
                new Employee {  OrganisationId = 2,  FirstName = "Lionel",         LastName = "Espinoza" },
              };

                context.Employee.AddRange(employee);
                context.SaveChanges();

                return;
            }
        }
    }
}
