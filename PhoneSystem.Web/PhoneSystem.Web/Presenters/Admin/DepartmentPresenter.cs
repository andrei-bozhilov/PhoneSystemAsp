namespace PhoneSystem.Web.Presenters.Admin
{
    using System.Linq;

    using AutoMapper.QueryableExtensions;

    using PhoneSystem.Models;
    using PhoneSystem.Web.ViewModels.Admin.Departments;

    public class DepartmentPresenter : BaseCrudPresenter<IQueryable<DepartmentViewModel>, Department>
    {
        public override IResult GetResult()
        {
            var model = this.Data.Departments.All()
                 .OrderBy(d => d.Name)
                 .Project().To<DepartmentViewModel>();

            return this.DataResult(model);
        }

        public override IResult GetById<TModel>(object id)
        {
            var model = this.Data.Departments.AllWithDeleted()
                .Where(x => x.Id == (int)id)
                .Project().To<TModel>()
                .FirstOrDefault();

            return this.DataResult(model);
        }

        public override IResult Add(Department entity)
        {
            this.Data.Departments.Add(entity);
            return this.SavaChanges("Successfully add department.");
        }

        public override IResult Update(Department entity)
        {
            this.Data.Departments.Update(entity);
            return this.SavaChanges("Successfully update department.");
        }

        public override IResult Delete(object id)
        {
            this.Data.Departments.Delete(id);
            return this.SavaChanges("Successfully delete department.");
        }

        public override IResult UnDelete(object id)
        {
            this.Data.Departments.UnDelete(id);
            return this.SavaChanges("Successfully undelete department.");
        }

        public IResult DataItemDeleted()
        {
            var result = this.Data.Departments.AllDeleted()
                .OrderByDescending(j => j.Name)
                .Project().To<DepartmentViewModel>();

            return this.DataResult(result);
        }
    }
}