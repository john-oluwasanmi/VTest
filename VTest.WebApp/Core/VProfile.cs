using AutoMapper;
using V.Test.Web.App.Entities;
using V.Test.Web.App.ViewModels;

namespace V.Test.Web.App.Core
{
    public class VProfile : Profile
    {
        public VProfile()
        {
            CreateMap<AddressViewModel, Address>(MemberList.None).ReverseMap();
            CreateMap<EmployeeViewModel, Employee>(MemberList.None).ReverseMap();
            CreateMap<OrganisationViewModel, Organisation>(MemberList.None).ReverseMap();
        }
    }
}
