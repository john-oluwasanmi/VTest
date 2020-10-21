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
    public class EmployeeController : VTestControllerBase<EmployeeViewModel, Employee, IEmployeeBusinessService>
    {
        private readonly IHtmlHelper _htmlHelper;

        public EmployeeController(ILogger<Employee> logger
                                , IEmployeeBusinessService  employeeBusinessService
                                , IConfiguration configuration)
            : base(logger, employeeBusinessService, configuration)
        {
        }
    }
}