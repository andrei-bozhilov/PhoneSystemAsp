namespace PhoneSystem.Web.Pages.Admin
{
    using PhoneSystem.Web.Presenters;

    public abstract class AdminCrudPage<VM, DM, P> : AdminBasePage<VM, P>
        where P : IPresenter<VM>, ICrudPresenter<DM>
    {
        protected void Create(DM model)
        {
            IResult result = this.Presenter.Add(model);
            this.TakeIResult(result);
        }

        protected void Edit(DM model)
        {
            IResult result = this.Presenter.Update(model);
            this.TakeIResult(result);
        }

        protected void Delete(object model)
        {
            IResult result = this.Presenter.Delete(model);
            this.TakeIResult(result);
        }

        protected void UnDelete(object model)
        {
            IResult result = this.Presenter.UnDelete(model);
            this.TakeIResult(result);
        }

        protected void GetById<TModel>(object id, ref TModel outModel)
        {
            IResult result = this.Presenter.GetById<TModel>(id);
            this.TakeIResult(result, ref outModel);
        }
    }
}