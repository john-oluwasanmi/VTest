﻿using System;
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
        public async Task<IActionResult> Index()
        {
            var organisationList = await ListOrganisations();
            var viewModel = new EmployeeViewModel { OrganisationList = organisationList };
            return View(viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm]EmployeeViewModel item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            await base.AddAsync(item);

            return RedirectToAction(nameof(EmployeeController.List), "Employee", new { organisationId  = item.OrganisationId});
        }

        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var viewModel = await base.GetAsync(id);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> List(int organisationId, int pageNumber = 1)
        {
            var entities = await BusinessServiceManager.ListByOrganisationAsync(organisationId, pageNumber);
            var viewModels = ConvertEntityToViewModel(entities);

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
        public async Task<IActionResult> Update([FromForm]EmployeeViewModel item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            await base.UpdateAsync(item);

            return RedirectToAction(nameof(EmployeeController.List), "Employee", new { organisationId = item.OrganisationId });
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await BusinessServiceManager.GetAsync(id);
            var viewModel = ConvertEntityToViewModel(entity);
            return View(viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromForm] EmployeeViewModel item)
        {
            await base.DeleteAsync(item);
            return RedirectToAction(nameof(EmployeeController.List), "Employee", new { organisationId = item.OrganisationId });
        }

        private async Task<List<OrganisationViewModel>> FetchOrganisation(int pagenumber=1)
        {
            List<Organisation> organisations = await _organisationBusinessService.ListAsync(pagenumber);
            List<OrganisationViewModel> organisationsViewModels = ConvertEntityToViewModel<Organisation, OrganisationViewModel>(organisations);
            return organisationsViewModels;
        }
        private async Task<IEnumerable<SelectListItem>> ListOrganisations(int pagenumber = 1)
        {
            var ordered = await FetchOrganisation(pagenumber);

            var organisationLists = from it in ordered
                                    select new SelectListItem
                                    {
                                        Text = it.OrganisationName,
                                        Value = it.Id.ToString()
                                    };

            return organisationLists;
        }
    }
}