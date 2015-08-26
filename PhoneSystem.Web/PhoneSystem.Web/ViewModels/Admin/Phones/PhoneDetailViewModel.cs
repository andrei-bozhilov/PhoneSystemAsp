namespace PhoneSystem.Web.ViewModels.Admin.Phones
{
    using System;

    using PhoneSystem.Infrastucture.Mapping;
    using PhoneSystem.Models;
    using PhoneSystem.Web.Controls.Attibutes;

    public class PhoneDetailViewModel : IMapFrom<Phone>, IHaveCustomMappings
    {
        [CanBeModified(false)]
        public int Id { get; set; }

        [CanBeModified(false)]
        public string PhoneNumber { get; set; }

        [CanBeModified(false)]
        public string Status { get; set; }

        [CanBeModified(false)]
        public bool HasRouming { get; set; }

        [CanBeModified(false)]
        public int? CreditLimit { get; set; }

        [CanBeModified(false)]
        public string CardType { get; set; }

        [CanBeModified(false)]
        public string UserName { get; set; }

        [CanBeModified(false)]
        public string FullName { get; set; }

        [CanBeModified(false)]
        public DateTime? AvailableAfter { get; set; }

        [CanBeModified(false)]
        public DateTime CreatedOn { get; set; }

        [CanBeModified(false)]
        public bool PreserveCreatedOn { get; set; }

        [CanBeModified(false)]
        public DateTime? ModifiedOn { get; set; }

        [CanBeModified(false)]
        public bool IsDeleted { get; set; }

        [CanBeModified(false)]
        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Phone, PhoneDetailViewModel>()
             .ForMember(m => m.CardType, opt => opt.MapFrom(u => u.CardType.ToString()));

            configuration.CreateMap<Phone, PhoneDetailViewModel>()
               .ForMember(m => m.Status, opt => opt.MapFrom(u => u.PhoneStatus.ToString()));

            configuration.CreateMap<Phone, PhoneDetailViewModel>()
               .ForMember(m => m.UserName, opt => opt.MapFrom(u => u.User.UserName));

            configuration.CreateMap<Phone, PhoneDetailViewModel>()
               .ForMember(m => m.FullName, opt => opt.MapFrom(u => u.User.FullName));
        }
    }
}