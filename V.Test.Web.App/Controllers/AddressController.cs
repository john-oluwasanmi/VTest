﻿using System;
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
    }
}