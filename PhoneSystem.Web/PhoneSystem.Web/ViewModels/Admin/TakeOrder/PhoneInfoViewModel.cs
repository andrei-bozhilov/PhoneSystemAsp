namespace PhoneSystem.Web.ViewModels.Admin.TakeOrder
{
    using System;

    using AutoMapper;

    using PhoneSystem.Infrastucture.Mapping;
    using PhoneSystem.Models;

    public class PhoneInfoViewModel : IMapFrom<Phone>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string PhoneNumber { get; set; }

        public string Status { get; set; }

        public bool HasRouming { get; set; }

        public int? CreditLimit { get; set; }

        public string CardType { get; set; }

        public DateTime CreatedAt { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Phone, PhoneInfoViewModel>()
               .ForMember(m => m.CardType, opt => opt.MapFrom(u => u.CardType.ToString()));

            configuration.CreateMap<Phone, PhoneInfoViewModel>()
               .ForMember(m => m.Status, opt => opt.MapFrom(u => u.PhoneStatus.ToString()));
        }
    }
}