namespace PhoneSystem.Web.Presenters.Admin
{
    using System.Linq;

    using AutoMapper.QueryableExtensions;

    using PhoneSystem.Web.ViewModels.Admin;
    using PhoneSystem.Models;
    using PhoneSystem.Web.ViewModels.Admin.JobTitles;

    public class JobTitlePresenter : BaseCrudPresenter<IQueryable<JobTitleViewModel>, JobTitle>
    {
        public override IResult GetResult()
        {
            var model = this.Data.JobTitles.All()
                .OrderByDescending(j => j.Name)
                .Project().To<JobTitleViewModel>();

            return this.DataResult(model);
        }

        public IResult DataItemDeleted()
        {
            var result = this.Data.JobTitles.AllDeleted()
               .OrderByDescending(j => j.Name)
               .Project().To<JobTitleViewModel>();

            return this.DataResult(result);
        }

        public override IResult GetById<TModel>(object id)
        {
            var model = this.Data.JobTitles.AllWithDeleted()
                .Where(x => x.Id == (int)id)
                .Project().To<TModel>()
                .FirstOrDefault();

            return this.DataResult(model);
        }

        public override IResult Add(JobTitle entity)
        {
            this.Data.JobTitles.Add(entity);
            return this.SavaChanges("Successfully created job title.");
        }

        public override IResult Update(JobTitle entity)
        {
            this.Data.JobTitles.Update(entity);
            return this.SavaChanges("Successfully updated job title.");
        }

        public override IResult Delete(object id)
        {
            this.Data.JobTitles.Delete(id);
            return this.SavaChanges("Successfully delete job title.");
        }

        public override IResult UnDelete(object id)
        {
            this.Data.JobTitles.UnDelete(id);
            return this.SavaChanges("Successfully undelete job title.");
        }
    }
}