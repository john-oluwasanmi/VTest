using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using V.Test.Web.Api.BusinessService.Interface;
using V.Test.Web.Api.Entities;
using V.Test.Web.App.ViewModels;

namespace V.Test.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : VTestControllerBase<EmployeeViewModel, Employee, IEmployeeBusinessService>
    {
        private readonly IHtmlHelper _htmlHelper;
        private readonly IOrganisationBusinessService _organisationBusinessService;

        public EmployeeController(ILogger<Employee> logger
                                , IEmployeeBusinessService employeeBusinessService
                                , IOrganisationBusinessService organisationBusinessService
                                , IConfiguration configuration)
            : base(logger, employeeBusinessService, configuration)
        {
            this._organisationBusinessService = organisationBusinessService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]

        [ProducesResponseType(404)]
        public async Task<IActionResult> ListAsync(int pageNumber = 1)
        {
            return await base.ListAsync(pageNumber);
        }


        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpGet("ListByOrganisationAsync/{organisationId}")]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ListByOrganisationAsync(int organisationId, int pageNumber = 1)
        {
            try
            {
                CurrentPageNumber = pageNumber;


                var entities = await BusinessServiceManager.ListByOrganisationAsync(organisationId, CurrentPageNumber);

                CurrentPageNumber = 0;

                if (entities == null || !entities.Any())
                {
                    return NotFound();
                }

                var result = IMapper.Map<List<Employee>, List<EmployeeViewModel>>(entities);

                return Ok(result);

            }
            catch (Exception ex)
            {
                LogException(ex);
                return StatusCode(500);
            }
        }


        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public new async Task<IActionResult> GetAsync(int id)
        {
            return await base.GetAsync(id);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public new async Task<IActionResult> AddAsync([FromBody]EmployeeViewModel item)
        {
            return await base.AddAsync(item);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] EmployeeViewModel item)
        {
            item.Id = id;
            return await base.UpdateAsync(item);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return await base.DeleteAsync(new EmployeeViewModel { Id = id });

        }
    }
}