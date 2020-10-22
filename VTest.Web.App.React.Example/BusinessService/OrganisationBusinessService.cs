using System;
using System.Collections.Generic;
using V.Test.Web.Api.BusinessService.Interface;
using V.Test.Web.Api.Entities;
using V.Test.Web.Api.Repository.Interface;

namespace V.Test.Web.Api.BusinessService
{
    public   class OrganisationBusinessService : BusinessServiceBase<Organisation, IOrganisationRepository>
        , IOrganisationBusinessService
    {
        public OrganisationBusinessService(IOrganisationRepository   organisationRepository)
           : base(organisationRepository)
        { }

    }
}
