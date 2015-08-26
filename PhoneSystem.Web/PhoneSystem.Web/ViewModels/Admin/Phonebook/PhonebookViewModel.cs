namespace PhoneSystem.Web.ViewModels.Admin.Phonebook
{
    using PhoneSystem.Infrastucture.Mapping;
    using PhoneSystem.Models;

    public class PhonebookViewModel : IMapFrom<Phone>, IHaveCustomMappings
    {
        public string PhoneNumber { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string DepartmentName { get; set; }

        public string JobTitle { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration
                .CreateMap<Phone, PhonebookViewModel>()
                .ForMember(to => to.UserName, opt => opt.MapFrom(from => from.User.UserName));

            configuration
                .CreateMap<Phone, PhonebookViewModel>()
                .ForMember(to => to.FullName, opt => opt.MapFrom(from => from.User.FullName));

            configuration
                .CreateMap<Phone, PhonebookViewModel>()
                .ForMember(to => to.DepartmentName, opt => opt.MapFrom(from => from.User.Department.Name));

            configuration
                .CreateMap<Phone, PhonebookViewModel>()
                .ForMember(to => to.JobTitle, opt => opt.MapFrom(from => from.User.JobTitle.Name));
        }
    }
}