namespace PhoneSystem.Web.Pages.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using PhoneSystem.Web.Presenters.Admin;


    public partial class Home : AdminBasePage<IQueryable<string>, HomePresenter>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.GetAdminMenuOptions(this.menu);
            }

            this.TakeViewModel(new HomePresenter());
        }

        public IEnumerable<string> users_GetData()
        {
            return this.ViewModel;
        }
    }
}