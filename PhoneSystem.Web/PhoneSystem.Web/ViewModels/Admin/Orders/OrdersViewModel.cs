namespace PhoneSystem.Web.ViewModels.Admin.Orders
{
    using System;

    using AutoMapper;

    using PhoneSystem.Infrastucture.Mapping;
    using PhoneSystem.Models;

    public class OrdersViewModel : IMapFrom<PhoneNumberOrder>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime ActionDate { get; set; }

        public string PhoneAction { get; set; }

        public string AdminUserName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<PhoneNumberOrder, OrdersViewModel>()
               .ForMember(from => from.UserName, opt => opt.MapFrom(to => to.User.UserName));

            configuration.CreateMap<PhoneNumberOrder, OrdersViewModel>()
               .ForMember(from => from.FullName, opt => opt.MapFrom(to => to.User.FullName));

            configuration.CreateMap<PhoneNumberOrder, OrdersViewModel>()
              .ForMember(from => from.PhoneAction, opt => opt.MapFrom(to => to.PhoneAction.ToString()));
        }
    }
}