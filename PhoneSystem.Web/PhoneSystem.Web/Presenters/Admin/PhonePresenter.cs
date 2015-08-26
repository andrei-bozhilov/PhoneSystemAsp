namespace PhoneSystem.Web.Presenters.Admin
{
    using System.Linq;

    using AutoMapper.QueryableExtensions;

    using PhoneSystem.Models;
    using PhoneSystem.Web.ViewModels.Admin.Phones;

    public class PhonePresenter : BaseCrudPresenter<IQueryable<PhoneViewModel>, Phone>
    {
        public override IResult GetResult()
        {
            var data = this.Data.Phones.All()
                .OrderBy(x => x.Id)
                .Project().To<PhoneViewModel>();

            return this.DataResult(data);
        }

        public override IResult GetById<TModel>(object id)
        {
            var data = this.Data.Phones.AllWithDeleted()
                .Where(x => x.Id == (int)id)
                .Project().To<TModel>()
                .FirstOrDefault();

            return this.DataResult(data);
        }

        public override IResult Add(Phone entity)
        {
            this.Data.Phones.Add(entity);
            return this.SavaChanges("Successfully created phone.");
        }

        public override IResult Update(Phone entity)
        {
            this.Data.Phones.Update(entity);
            return this.SavaChanges("Successfully update phone.");
        }

        public override IResult Delete(object id)
        {
            this.Data.Phones.Delete(id);
            return this.SavaChanges("Successfully delete phone.");
        }

        public override IResult UnDelete(object id)
        {
            this.Data.Phones.UnDelete(id);
            return this.SavaChanges("Successfully undelete phone.");
        }
    }
}