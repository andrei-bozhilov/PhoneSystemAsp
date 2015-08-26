namespace PhoneSystem.Web.Presenters
{
    using PhoneSystem.Common;
    using PhoneSystem.Web.Presenters.Results;

    public abstract class BaseCrudPresenter<VM, DM> : BasePresenter<VM>, ICrudPresenter<DM>
    {
        public abstract IResult Add(DM entity);

        public abstract IResult Update(DM entity);

        public abstract IResult Delete(object id);

        public abstract IResult UnDelete(object id);

        protected IResult SavaChanges(string successMessage,
            string errorMessage = GlobalConstants.ErrorMessage)
        {
            if (this.Data.SaveChanges() > 0)
            {
                return new MessageResult(successMessage);
            }
            else
            {
                return new ErrorResult(errorMessage);
            }
        }
    }
}