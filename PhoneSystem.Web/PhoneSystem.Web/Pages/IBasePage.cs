namespace PhoneSystem.Web.Pages
{
    using PhoneSystem.Web.Presenters;

    public interface IBasePage<VM, P> where P : IPresenter<VM>
    {
        P Presenter { get; set; }

        VM ViewModel { get; set; }

        void TakeViewModel(P presenter);
    }
}
