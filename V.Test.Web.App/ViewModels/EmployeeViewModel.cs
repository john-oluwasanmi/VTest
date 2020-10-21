﻿using System;
using System.Collections.Generic;
using V.Test.Web.Api.Entities;

namespace V.Test.Web.App.ViewModels
{
    public partial class EmployeeViewModel : ViewModelBase
    {
        public int OrganisationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}