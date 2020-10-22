using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
 
using V.Test.Web.Api.BusinessService.Interface;
using V.Test.Web.Api.Entities;
using V.Test.Web.Api.Repository.Interface;

namespace V.Test.Web.Api.BusinessService
{
    public   class AddressBusinessService : BusinessServiceBase<Address, IAddressRepository>
        , IAddressBusinessService
    {
        public AddressBusinessService(IAddressRepository   addressRepository)
           : base(addressRepository)
        { }

    }
}
