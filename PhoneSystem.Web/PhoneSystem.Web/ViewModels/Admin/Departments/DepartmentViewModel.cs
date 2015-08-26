namespace PhoneSystem.Web.ViewModels.Admin.Departments
{
    using PhoneSystem.Infrastucture.Mapping;
    using PhoneSystem.Models;

    public class DepartmentViewModel : IMapFrom<Department>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}