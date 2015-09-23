namespace PhoneSystem.Web.Pages.Admin
{
    using System;
    using Presenters.Admin;

    public partial class Tables : AdminBasePage<object, TablePresenter>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.GetAdminMenuOptions(this.menu);
            this.GetSubMenuOptions(
                 this.menu,
                 "Tables",
                 this.subMenu,
                 new string[] { "Users", "Phones", "Departments", "JobTitles" });
        }
    }
}