namespace PhoneSystem.Web.ViewModels.Admin.UserDetails
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper.QueryableExtensions;

    using PhoneSystem.Infrastucture.Mapping;
    using PhoneSystem.Models;
    using PhoneSystem.Web.ViewModels.Admin.Orders;

    public class UserDetailInfoViewModel : IMapFrom<User>//, IHaveCustomMappings
    {
        public UserInfoViewModel UserInfo { get; set; }

        public IQueryable<OrdersViewModel> OrdersInfo { get; set; }

        public IQueryable<PhoneInfoViewModel> PhonesInfo { get; set; }

        //public void CreateMappings(AutoMapper.IConfiguration configuration)
        //{
        //    configuration.CreateMap<User, UserDetailInfoViewModel>()
        //     .ForMember(d => d.UserInfo, opt => opt.MapFrom(from => from));


        //    configuration.CreateMap<User, UserDetailInfoViewModel>()
        //    .ForMember(d => d.OrdersInfo,
        //    opt => opt.MapFrom(from => from.UserPhoneNumberOrders
        //                                    .Where(order => order.UserId == this.UserInfo.Id)
        //                                    .OrderByDescending(order => order.ActionDate)));

        //    configuration.CreateMap<User, UserDetailInfoViewModel>()
        //    .ForMember(to => to.PhonesInfo, opt => opt.MapFrom(from => from));
        //}
    }
}