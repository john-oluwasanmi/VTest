using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using V.Test.Web.Api.Entities;

namespace V.Test.Web.Api.Repository
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
                 new Address{ AddressLine1 ="1 Churchill Place", AddressLine2="", AddressLine3="",AddressLine4="", Town ="London", Postcode="E14 5HP" ,CreatedOn=DateTime.UtcNow}
                ,new Address{ AddressLine1 ="8 Canada Square", AddressLine2="", AddressLine3="",AddressLine4="", Town ="London", Postcode="E14 5HQ",CreatedOn=DateTime.UtcNow }
                ,new Address{ AddressLine1 ="25 Gresham Street", AddressLine2="", AddressLine3="",AddressLine4="", Town ="London", Postcode="EC2V 7HN",CreatedOn=DateTime.UtcNow }
                ,new Address{ AddressLine1 ="Henry Duncan House", AddressLine2="120 George Street", AddressLine3="",AddressLine4="", Town ="Edinburgh", Postcode="EH2 4LH",CreatedOn=DateTime.UtcNow }
                ,new Address{ AddressLine1 ="30 St Vincent Place", AddressLine2="", AddressLine3="",AddressLine4="", Town ="Glasgow", Postcode="EC2V 5DD" ,CreatedOn=DateTime.UtcNow}
                ,new Address{ AddressLine1 ="1 Basinghall Avenue", AddressLine2="", AddressLine3="",AddressLine4="", Town ="London", Postcode="G1 2HL",CreatedOn=DateTime.UtcNow }
                ,new Address{ AddressLine1 ="The Mound", AddressLine2="", AddressLine3="",AddressLine4="", Town ="Edinburgh", Postcode="EH1 1YZ",CreatedOn=DateTime.UtcNow }
              };

                context.Address.AddRange(addresses);
                context.SaveChanges();
            }


            if (!context.Organisation.Any())
            {
                var organisation = new List<Organisation>
               {
                 new Organisation{OrganisationName="Barclays UK PLC", OrganisationNumber="09740322", AddressId=  1,CreatedOn=DateTime.UtcNow}
                ,new Organisation{OrganisationName="HSBC BANK PLC", OrganisationNumber="00014259", AddressId=2 ,CreatedOn=DateTime.UtcNow }
                ,new Organisation{OrganisationName="LLOYDS BANK PLC", OrganisationNumber="00002065", AddressId= 3 ,CreatedOn=DateTime.UtcNow}
                ,new Organisation{OrganisationName="TSB BANK PLC", OrganisationNumber="SC095237", AddressId=4 ,CreatedOn=DateTime.UtcNow}
                ,new Organisation{OrganisationName="CLYDESDALE BANK PLC", OrganisationNumber="SC001111", AddressId=5 ,CreatedOn=DateTime.UtcNow}
                ,new Organisation{OrganisationName="STANDARD CHARTERED PLC", OrganisationNumber="00966425", AddressId= 6 ,CreatedOn=DateTime.UtcNow}
                ,new Organisation{OrganisationName="BANK OF SCOTLAND PLC", OrganisationNumber="SC327000", AddressId=7  ,CreatedOn=DateTime.UtcNow}
              };

                context.Organisation.AddRange(organisation);
                context.SaveChanges();
            }

            if (!context.Employee.Any())
            {
                var employee = new List<Employee>
               {
                new Employee { OrganisationId = 1, FirstName = "Janet", LastName = "Smith",CreatedOn=DateTime.UtcNow },
                  new Employee { OrganisationId = 1,    FirstName="Frank",LastName = "Bloswick",CreatedOn=DateTime.UtcNow},
                 new Employee { OrganisationId = 1,    FirstName= "Tonya",LastName = "Bazinaw",CreatedOn=DateTime.UtcNow},
                new Employee { OrganisationId = 1,    FirstName= "Mike",LastName = "St. Onge",CreatedOn=DateTime.UtcNow},
                new Employee { OrganisationId = 1,    FirstName= "Jackie",LastName = "Jones",CreatedOn=DateTime.UtcNow},
                new Employee { OrganisationId = 1,    FirstName= "Darren",LastName = "Tillbrooke",CreatedOn=DateTime.UtcNow},
                new Employee { OrganisationId = 1,    FirstName= "Stephanie",LastName = "Holsinger",CreatedOn=DateTime.UtcNow},
                new Employee { OrganisationId = 1,    FirstName= "Rene",LastName = "Hughey",CreatedOn=DateTime.UtcNow},
                new Employee { OrganisationId = 1,    FirstName= "Robert",LastName = "Rogers",CreatedOn=DateTime.UtcNow},
                new Employee { OrganisationId = 1,    FirstName= "Richard",LastName = "LaPine",CreatedOn=DateTime.UtcNow},
                new Employee { OrganisationId = 1,    FirstName= "Kathy",LastName = "Summerfield",CreatedOn=DateTime.UtcNow},
                new Employee{OrganisationId = 1,    FirstName = "Kathy",LastName = "Bodwin",CreatedOn=DateTime.UtcNow},


                new Employee{OrganisationId = 3, FirstName = "Mitch",LastName = "Krause",CreatedOn=DateTime.UtcNow },
                new Employee{OrganisationId = 3, FirstName = "George",LastName = "Dow" ,CreatedOn=DateTime.UtcNow},
                new Employee{OrganisationId = 3, FirstName = "Jack",LastName = "Malone",CreatedOn=DateTime.UtcNow },
                new Employee{OrganisationId = 3, FirstName = "Bill",LastName = "Schweiz",CreatedOn=DateTime.UtcNow },
                new Employee{OrganisationId = 3, FirstName = "Mark",LastName = "Gunter" ,CreatedOn=DateTime.UtcNow},
                new Employee{OrganisationId = 3, FirstName = "Bob",LastName = "Anderson" ,CreatedOn=DateTime.UtcNow},
                new Employee{OrganisationId = 3, FirstName = "Scott",LastName = "Simpson",CreatedOn=DateTime.UtcNow },
                new Employee{OrganisationId = 3, FirstName = "Phil",LastName = "ingman" ,CreatedOn=DateTime.UtcNow},
                new Employee{OrganisationId = 3, FirstName = "Chad",LastName = "Leiker",CreatedOn=DateTime.UtcNow },
                new Employee{OrganisationId = 3, FirstName = "Ian",LastName = "Benson" ,CreatedOn=DateTime.UtcNow},
                new Employee{OrganisationId = 3, FirstName = "Nicole",LastName = "Lane" ,CreatedOn=DateTime.UtcNow},
                new Employee{OrganisationId = 3, FirstName = "Steve",LastName = "Lundeen",CreatedOn=DateTime.UtcNow },
                new Employee{OrganisationId = 3, FirstName = "Erica",LastName = "Black",CreatedOn=DateTime.UtcNow },
                new Employee{OrganisationId = 3, FirstName = "Xenos",LastName = "Mathis",CreatedOn=DateTime.UtcNow },
                new Employee{OrganisationId = 3, FirstName = "Kyle",LastName = "Good" ,CreatedOn=DateTime.UtcNow},
                new Employee{OrganisationId = 3, FirstName = "Raja",LastName = "Dejesus",CreatedOn=DateTime.UtcNow },
                new Employee{OrganisationId = 3, FirstName = "Timothy",LastName = "Frazier" ,CreatedOn=DateTime.UtcNow},
                new Employee{OrganisationId = 3, FirstName = "Francine",LastName = "Morrison",CreatedOn=DateTime.UtcNow },

                new Employee{OrganisationId = 4,FirstName = "Avram",    LastName = "Pate",CreatedOn=DateTime.UtcNow },
                new Employee{OrganisationId = 4,FirstName = "Hammett",    LastName = "Coffey",CreatedOn=DateTime.UtcNow },
                new Employee{OrganisationId = 4,FirstName = "Hasad",    LastName = "Wise" ,CreatedOn=DateTime.UtcNow},
                new Employee{OrganisationId = 4,FirstName = "Cullen",    LastName = "Riddle",CreatedOn=DateTime.UtcNow },
                new Employee{OrganisationId = 4,FirstName = "Kato",    LastName = "Delgado",CreatedOn=DateTime.UtcNow },
                new Employee{OrganisationId = 4,FirstName = "Todd",    LastName = "Wright",CreatedOn=DateTime.UtcNow },
                new Employee{OrganisationId = 4,FirstName = "Troy",    LastName = "Mccoy",CreatedOn=DateTime.UtcNow },
                new Employee{OrganisationId = 4,FirstName = "Gil",    LastName = "Duncan" ,CreatedOn=DateTime.UtcNow},
                new Employee{OrganisationId = 4,FirstName = "Lionel",    LastName = "Espinoza",CreatedOn=DateTime.UtcNow },
                new Employee{ OrganisationId = 4,    FirstName = "Victor",    LastName = "Merrill" ,CreatedOn=DateTime.UtcNow},
                new Employee{OrganisationId = 5,FirstName = "Gennifer",     LastName = "Vance" ,CreatedOn=DateTime.UtcNow},
                new Employee{OrganisationId = 5,FirstName = "Chancellor",     LastName = "Warner",CreatedOn=DateTime.UtcNow },
                new Employee { OrganisationId = 5,     FirstName = "Davis",     LastName = "Wolf",CreatedOn=DateTime.UtcNow },
                new Employee{OrganisationId = 6,FirstName = "Carlos",         LastName = "Clarke",CreatedOn=DateTime.UtcNow },
                new Employee{OrganisationId = 6,FirstName = "Dolan",         LastName = "Mercado" ,CreatedOn=DateTime.UtcNow},
                new Employee{OrganisationId = 6,FirstName = "Helen",         LastName = "Guthrie" ,CreatedOn=DateTime.UtcNow},
                new Employee{OrganisationId = 6,FirstName = "Elmo",         LastName = "Douglas" ,CreatedOn=DateTime.UtcNow},
                new Employee{OrganisationId = 6,FirstName = "Kane",         LastName = "Rice",CreatedOn=DateTime.UtcNow },
                new Employee{OrganisationId = 6,FirstName = "Colt",         LastName = "Rowland",CreatedOn=DateTime.UtcNow },
                new Employee{OrganisationId = 6,FirstName = "John",         LastName = "Rose" ,CreatedOn=DateTime.UtcNow},
                new Employee{OrganisationId = 6,FirstName = "Alfonso",         LastName = "Hopkins" ,CreatedOn=DateTime.UtcNow},
                new Employee{OrganisationId = 6,FirstName = "Ida",         LastName = "Watts",CreatedOn=DateTime.UtcNow },
                new Employee{OrganisationId = 6,FirstName = "Jennifer",         LastName = "Coleman" },
                new Employee{OrganisationId = 6,FirstName = "Ciaran",         LastName = "Newton",CreatedOn=DateTime.UtcNow },
                new Employee{OrganisationId = 6,FirstName = "Hiram",         LastName = "Carrillo",CreatedOn=DateTime.UtcNow },
                new Employee{OrganisationId = 6,FirstName = "Devin",         LastName = "Russell",CreatedOn=DateTime.UtcNow },
                new Employee{OrganisationId = 6,FirstName = "Arsenio",         LastName = "Jensen",CreatedOn=DateTime.UtcNow },
                new Employee{OrganisationId = 6,FirstName = "Otto",         LastName = "Gibbs",CreatedOn=DateTime.UtcNow },
                new Employee {OrganisationId = 6, FirstName = "Hiram",         LastName = "Vega" ,CreatedOn=DateTime.UtcNow},
                new Employee {  OrganisationId = 7,  FirstName = "Jarrod",         LastName = "Randolph",CreatedOn=DateTime.UtcNow },
                new Employee {  OrganisationId = 7,  FirstName = "Josiah",         LastName = "Gates",CreatedOn=DateTime.UtcNow },
                new Employee {  OrganisationId = 7,  FirstName = "Brandon",         LastName = "Stanley",CreatedOn=DateTime.UtcNow },
                new Employee {  OrganisationId = 7,  FirstName = "Kennedy",         LastName = "Nunez",CreatedOn=DateTime.UtcNow },
                new Employee {  OrganisationId = 7,  FirstName = "Lewis",         LastName = "Sanchez" ,CreatedOn=DateTime.UtcNow},
                new Employee {  OrganisationId = 7,  FirstName = "Kassie",         LastName = "Chaney" ,CreatedOn=DateTime.UtcNow},
                new Employee {  OrganisationId = 7,  FirstName = "Lance",         LastName = "Knox" ,CreatedOn=DateTime.UtcNow},
                new Employee {  OrganisationId = 7,  FirstName = "Lamar",         LastName = "Harrison" ,CreatedOn=DateTime.UtcNow},
                new Employee {  OrganisationId = 7,  FirstName = "Honorato",         LastName = "Montgomery" ,CreatedOn=DateTime.UtcNow},
                new Employee {  OrganisationId = 2,  FirstName = "Lisa",         LastName = "Nielsen",CreatedOn=DateTime.UtcNow },
                new Employee {  OrganisationId = 2,  FirstName = "Layla",         LastName = "Barr" ,CreatedOn=DateTime.UtcNow},
                new Employee {  OrganisationId = 2,  FirstName = "Nancy",         LastName = "Mcclain" ,CreatedOn=DateTime.UtcNow},
                new Employee {  OrganisationId = 2,  FirstName = "Kato",         LastName = "Delgado",CreatedOn=DateTime.UtcNow },
                new Employee {  OrganisationId = 2,  FirstName = "Todd",         LastName = "Wright" ,CreatedOn=DateTime.UtcNow},
                new Employee {  OrganisationId = 2,  FirstName = "Troy",         LastName = "Mccoy",CreatedOn=DateTime.UtcNow},
                new Employee {  OrganisationId = 2,  FirstName = "Gil",         LastName = "Duncan" ,CreatedOn=DateTime.UtcNow},
                new Employee {  OrganisationId = 2,  FirstName = "Lionel",         LastName = "Espinoza",CreatedOn=DateTime.UtcNow },
              };

                context.Employee.AddRange(employee);
                context.SaveChanges();

                return;
            }
        }
    }
}
