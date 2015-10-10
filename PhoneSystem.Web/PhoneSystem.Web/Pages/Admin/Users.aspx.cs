namespace PhoneSystem.Web.Pages.Admin
{
    using System;
    using System.Linq;

    using PhoneSystem.Web.Presenters.Admin;
    using PhoneSystem.Web.ViewModels.Admin.GiveOrder;

    public partial class Users : AdminBasePage<IQueryable<UserViewModel>, UserPresenter>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.TakeViewModel(new UserPresenter());
            this.GridUsers.NeedDataSource += GridUsers_NeedDataSource;

            if (!this.IsPostBack)
            {
                this.GetAdminMenuOptions(this.menu);
                this.GetSubMenuOptions(
                     this.menu,
                     "Tables",
                     this.subMenu,
                     new string[] { "Users", "Phones", "Departments", "JobTitles" });
            }
        }

        private void GridUsers_NeedDataSource(object sender, EventArgs e)
        {
            this.GridUsers.DataSource = this.ViewModel;
        }


        protected void GridUsers_BtnViewClick(object sender, EventArgs e)
        {
            string id = this.GetIdFromBtnSender(sender);
            this.TakeIResult(this.Presenter.GetById<string>(id));
        }
    }
}