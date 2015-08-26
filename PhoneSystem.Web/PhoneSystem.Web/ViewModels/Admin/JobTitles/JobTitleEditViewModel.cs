namespace PhoneSystem.Web.ViewModels.Admin.JobTitles
{
    using System;

    using PhoneSystem.Web.Controls.Attibutes;
    using PhoneSystem.Models;
    using PhoneSystem.Infrastucture.Mapping;

    public class JobTitleEditViewModel : IMapFrom<JobTitle>
    {
        [CanBeModifiedAttribute(false)]
        public int Id { get; set; }

        [CanBeModifiedAttribute(true)]
        public string Name { get; set; }
    }
}