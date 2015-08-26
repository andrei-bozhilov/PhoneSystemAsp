namespace PhoneSystem.Web.Presenters
{
    using Ninject;

    using PhoneSystem.Data.DbContext;
    using PhoneSystem.Data.UnitOfWork;
    using PhoneSystem.Web.Presenters.Results;

    public abstract class BasePresenter<T> : IPresenter<T>
    {
        public BasePresenter()
        {
            this.Data = new PhoneSystemData(new PhoneSystemDbContext());
        }
        //[Inject]
        public IPhoneSystemData Data { get; private set; }

        public abstract IResult GetResult();

        public abstract IResult GetById<TModel>(object id);

        protected IResult DataResult<TModel>(TModel model)
        {
            return new DataResult<TModel>(model);
        }

        protected IResult ErrorResult(string error)
        {
            return new ErrorResult(error);
        }

        protected IResult ErrorResult(string error, string redirectUrl)
        {
            return new ErrorResult(error, redirectUrl);
        }

        protected IResult MessageResult(string message)
        {
            return new MessageResult(message);
        }

        protected IResult MessageResult(string message, string redirectUrl)
        {
            return new MessageResult(message, redirectUrl);
        }

        protected IResult RedirectResult(string url)
        {
            return new RedirectResult(url);
        }
    }
}