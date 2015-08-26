namespace PhoneSystem.Web.ViewModels.Admin.UserDetails
{
    using System.Collections.Generic;

    using PhoneSystem.Infrastucture.Mapping;
    using PhoneSystem.Models;
    using PhoneSystem.Web.Controls.Attibutes;
    using System.Linq;

    public class UserInfoViewModel : IMapFrom<User>, IMapTo<User>, IHaveCustomMappings
    {
        [CanBeModified(false)]
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string DepartmentName { get; set; }

        public string JobTitleName { get; set; }

        public bool IsActive { get; set; }

        [NotMapProperty]
        public bool IsAdmin { get; set; }

        public int EmployeeNumber { get; set; }

        [SelectCollection("DepartmentName", "(Choose department)")]
        public IEnumerable<KeyValuePair> Departments { get; set; }

        [SelectCollection("JobTitleName", "(Choose job title)")]
        public IEnumerable<KeyValuePair> JobTitles { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            string adminRoleId = string.Empty;
            configuration.CreateMap<User, UserInfoViewModel>()
                .ForMember(d => d.IsAdmin, opt => opt.MapFrom(to => to.Roles.Any(x => x.RoleId == adminRoleId)));

            configuration.CreateMap<UserInfoViewModel, User>()
                .ForMember(d => d.DepartmentId, opt => opt.MapFrom(to => to.DepartmentName));

            configuration.CreateMap<UserInfoViewModel, User>()
                .ForMember(d => d.JobTitleId, opt => opt.MapFrom(to => to.JobTitleName));
        }
    }
}