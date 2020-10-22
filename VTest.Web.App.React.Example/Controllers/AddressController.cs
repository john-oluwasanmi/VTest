using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using V.Test.Web.Api.BusinessService;
using V.Test.Web.Api.BusinessService.Interface;
using V.Test.Web.Api.Entities;
using V.Test.Web.App.ViewModels;

namespace V.Test.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class AddressController : VTestControllerBase<AddressViewModel, Address, IAddressBusinessService>
    {
        private readonly IAddressBusinessService _addressBusinessService;

        public AddressController(ILogger<Address> logger
                                , IAddressBusinessService addressBusinessService
                                , IConfiguration configuration)
            : base(logger, addressBusinessService, configuration)
        {
            this._addressBusinessService = addressBusinessService;
        }



        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]

        [ProducesResponseType(404)]
        public async Task<IActionResult> ListAsync(int branchId, int pageNumber)
        {
            return await base.ListAsync(pageNumber);
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
        public new async Task<IActionResult> AddAsync([FromBody]AddressViewModel item)
        {
            return await base.AddAsync(item);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] AddressViewModel item)
        {
            item.Id = id;
            return await base.UpdateAsync(item);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return await base.DeleteAsync(new AddressViewModel { Id = id });
          
        }
    }
}