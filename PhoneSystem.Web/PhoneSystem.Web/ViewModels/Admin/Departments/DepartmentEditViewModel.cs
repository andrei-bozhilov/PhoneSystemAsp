namespace PhoneSystem.Web.ViewModels.Admin.Departments
{
    using PhoneSystem.Infrastucture.Mapping;
    using PhoneSystem.Models;
    using PhoneSystem.Web.Controls.Attibutes;

    public class DepartmentEditViewModel : IMapFrom<Department>
    {
        [CanBeModified(false)]
        public int Id { get; set; }

        [CanBeModified(true)]
        public string Name { get; set; }
    }
}