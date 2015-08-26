namespace PhoneSystem.Web.Presenters
{
    public interface ICrudPresenter<DM>
    {
        IResult Add(DM model);

        IResult Update(DM model);

        IResult Delete(object id);

        IResult UnDelete(object id);
    }
}