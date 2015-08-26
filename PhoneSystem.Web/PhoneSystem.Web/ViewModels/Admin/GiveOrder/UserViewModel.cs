namespace PhoneSystem.Web.ViewModels.Admin.GiveOrder
{
    using System.Linq;

    using AutoMapper;

    using PhoneSystem.Infrastucture.Mapping;
    using PhoneSystem.Models;
    using PhoneSystem.Common;

    public class UserViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string DepartmentName { get; set; }

        public string JobTitleName { get; set; }

        public bool IsActive { get; set; }

        public bool IsAdmin { get; set; }

        public int EmployeeNumber { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            string adminId = string.Empty;
            configuration.CreateMap<User, UserViewModel>()
              .ForMember(to => to.IsAdmin, opt => opt.MapFrom(from => from.Roles.Any(x => x.RoleId == adminId)));
        }
    }
}