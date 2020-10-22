using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using V.Test.Web.App.BusinessService;
using V.Test.Web.App.BusinessService.Interface;
using V.Test.Web.App.Entities;
using V.Test.Web.App.ViewModels;

namespace V.Test.Web.App.Controllers
{

    public class AddressController : VTestControllerBase<AddressViewModel, Address, IAddressBusinessService>
    {
        private readonly IAddressBusinessService _addressBusinessService;
        private readonly IHtmlHelper _htmlHelper;

        public AddressController(ILogger<Address> logger
                                , IAddressBusinessService addressBusinessService
                                , IConfiguration configuration)
            : base(logger, addressBusinessService, configuration)
        {
            this._addressBusinessService = addressBusinessService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new AddressViewModel { };
            return View(viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm]AddressViewModel item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            await base.AddAsync(item);

            return RedirectToAction(nameof(AddressController.List), "Address");
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
            var viewModel = await BusinessServiceManager.GetAsync(id);
            return View(viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm]AddressViewModel item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            await base.UpdateAsync(item);

            return RedirectToAction(nameof(AddressController.List), "Address");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await BusinessServiceManager.GetAsync(id);
            var viewModel = ConvertEntityToViewModel(entity);
            return View(viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromForm] AddressViewModel item)
        {
            await base.DeleteAsync(item);
            return RedirectToAction(nameof(AddressController.List), "Address");
        }
    }
}