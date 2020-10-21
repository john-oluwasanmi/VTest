using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
 
using V.Test.Web.App.BusinessService.Interface;
using V.Test.Web.App.Entities;
using V.Test.Web.App.Repository.Interface;

namespace V.Test.Web.App.BusinessService
{
    public   class AddressBusinessService : BusinessServiceBase<Address, IAddressRepository>
        , IAddressBusinessService
    {
        public AddressBusinessService(IAddressRepository   addressRepository)
           : base(addressRepository)
        { }

    }
}
