namespace PhoneSystem.Web.Presenters
{
    public interface IPresenter<T>
    {
        IResult GetResult();

        IResult GetById<TModel>(object id);
    }
}
