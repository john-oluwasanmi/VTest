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
        private readonly IAddressBusinessService _addressBusinessService;

        public OrganisationController(ILogger<Organisation> logger
                                , IOrganisationBusinessService organisationBusinessService
                                , IAddressBusinessService addressBusinessService
                                , IConfiguration configuration)
            : base(logger, organisationBusinessService, configuration)
        {
            this._addressBusinessService = addressBusinessService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new OrganisationViewModel { AddressId = 0, Address = new Address { Id = 0 } };
            return View(viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm]OrganisationViewModel item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            await base.AddAsync(item);

            return RedirectToAction(nameof(OrganisationController.List), "Organisation");
        }

        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var viewModel = await base.GetAsync(id);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> List(int pageNumber = 1)
        {
            var viewModels = await base.ListAsync(pageNumber); ;
            return View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var entity = await BusinessServiceManager.GetAsync(id);
            var viewModel = ConvertEntityToViewModel(entity);
            return View(viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm]OrganisationViewModel item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            await UpdateAddress(item);
            await base.UpdateAsync(item);

            return RedirectToAction(nameof(OrganisationController.List), "Organisation");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await BusinessServiceManager.GetAsync(id);
            var viewModel = ConvertEntityToViewModel(entity);
            return View(viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromForm] OrganisationViewModel item)
        {
            await base.DeleteAsync(item);
            return RedirectToAction(nameof(OrganisationController.List), "Organisation");
        }

        private async Task UpdateAddress(OrganisationViewModel item)
        {
            var newAddress = item.Address;
            var oldAddress = await _addressBusinessService.GetAsync(item.AddressId ?? 0);

            var createdDate = oldAddress.CreatedOn;
            newAddress.CreatedOn = createdDate;

            SetAuditInformation<Address>(newAddress, isUpdate: true);
            await _addressBusinessService.UpdateAsync(newAddress);
        }

    }
}