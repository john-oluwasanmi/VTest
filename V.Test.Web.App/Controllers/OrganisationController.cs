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
                                , IOrganisationBusinessService organisationBusinessService
                                , IConfiguration configuration)
            : base(logger, organisationBusinessService, configuration)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new OrganisationViewModel { };
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
        public async Task<IActionResult> List( int pageNumber = 1)
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
        public async Task<IActionResult> Update([FromForm]OrganisationViewModel item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

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
    }
}