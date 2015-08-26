namespace PhoneSystem.Web.Pages.Admin
{
    using System;
    using System.Linq;

    using PhoneSystem.Web.Presenters.Admin;
    using PhoneSystem.Web.ViewModels.Admin;
    using PhoneSystem.Web.Controls;
    using PhoneSystem.Web.ViewModels.Admin.GiveOrder;

    public partial class Users : AdminBasePage<IQueryable<UserViewModel>, UserPresenter>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.GetAdminMenuOptions(this.menu);
                this.GetSubMenuOptions(
                     this.menu,
                     "Tables",
                     this.subMenu,
                     new string[] { "Users", "Phones", "Departments", "JobTitles" });
            }

            this.TakeViewModel(new UserPresenter());
            this.GridUsers.GridButtons = GridButtons.View;
            this.GridUsers.ShowId = false;
            this.GridUsers.OnBtnViewClicked += GridUsers_OnBtnViewClicked;
            this.GridUsers.GetData(this.ViewModel);
            this.GridUsers.ShowViewModel = false;
        }

        void GridUsers_OnBtnViewClicked(object sender, EventArgs e)
        {
            string id = this.GetIdFromBtnSender(sender);
            this.TakeIResult(this.Presenter.GetById<string>(id));
        }
    }
}