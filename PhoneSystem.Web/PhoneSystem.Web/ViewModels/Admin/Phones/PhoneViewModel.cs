namespace PhoneSystem.Web.ViewModels.Admin.Phones
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using PhoneSystem.Infrastucture.Mapping;
    using PhoneSystem.Models;
    using PhoneSystem.Web.Controls.Attibutes;

    public class PhoneViewModel : IMapFrom<Phone>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string PhoneNumber { get; set; }

        [EnumCollection(typeof(PhoneStatus))]
        public string PhoneStatus { get; set; }

        public bool HasRouming { get; set; }

        public int? CreditLimit { get; set; }

        [EnumCollection(typeof(CardType))]
        public string CardType { get; set; }

        public string UserName { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Phone, PhoneViewModel>()
               .ForMember(from => from.PhoneStatus, opt => opt.MapFrom(to => to.PhoneStatus.ToString()));

            configuration.CreateMap<Phone, PhoneViewModel>()
               .ForMember(from => from.CardType, opt => opt.MapFrom(to => to.CardType.ToString()));

            configuration.CreateMap<Phone, PhoneViewModel>()
              .ForMember(from => from.UserName, opt => opt.MapFrom(to => to.User.UserName));
        }
    }
}