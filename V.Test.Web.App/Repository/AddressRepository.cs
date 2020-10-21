﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using V.Test.Web.App.Entities;
using V.Test.Web.App.Repository.Interface;

namespace V.Test.Web.App.Repository
{
    public   class AddressRepository : RepositoryBase<Address>,
        IAddressRepository
    {
        public AddressRepository(IConfiguration configuration, ILogger<Address> logger)
              : base(configuration, logger)
        {
        }

    }
}
