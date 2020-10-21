using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using V.Test.Web.App.BusinessService.Interface;
using V.Test.Web.App.Entities;
using V.Test.Web.App.ViewModels;

namespace V.Test.Web.App.Controllers
{
    public class OrganisationController : VTestControllerBase<OrganisationViewModel, Organisation, IOrganisationBusinessService>
    {
        private readonly IHtmlHelper _htmlHelper;

        public OrganisationController(ILogger<Organisation> logger
                                , IOrganisationBusinessService  organisationBusinessService
                                , IConfiguration configuration)
            : base(logger, organisationBusinessService, configuration)
        {
        }
    }
}