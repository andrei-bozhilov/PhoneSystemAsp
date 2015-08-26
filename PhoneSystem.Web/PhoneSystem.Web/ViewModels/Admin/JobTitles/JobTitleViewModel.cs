namespace PhoneSystem.Web.ViewModels.Admin.JobTitles
{
    using PhoneSystem.Infrastucture.Mapping;
    using PhoneSystem.Models;

    public class JobTitleViewModel : IMapFrom<JobTitle>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}