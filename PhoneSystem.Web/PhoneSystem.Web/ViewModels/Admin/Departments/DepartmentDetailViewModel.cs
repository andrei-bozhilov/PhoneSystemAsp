namespace PhoneSystem.Web.ViewModels.Admin.Departments
{
    using System;

    using PhoneSystem.Models;
    using PhoneSystem.Infrastucture.Mapping;
    using PhoneSystem.Web.Controls.Attibutes;

    public class DepartmentDetailViewModel : IMapFrom<Department>
    {
        [CanBeModified(false)]
        public int Id { get; set; }

        [CanBeModified(false)]
        public string Name { get; set; }

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
    }
}