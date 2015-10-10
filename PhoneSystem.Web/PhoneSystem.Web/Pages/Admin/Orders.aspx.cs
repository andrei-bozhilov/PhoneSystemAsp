namespace PhoneSystem.Web.Pages.Admin
{
    using System;
    using System.Linq;

    using PhoneSystem.Web.Presenters.Admin;
    using PhoneSystem.Web.ViewModels.Admin.Orders;

    public partial class Orders : AdminBasePage<IQueryable<OrdersViewModel>, OrderPresenter>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.TakeViewModel(new OrderPresenter());

            if (!this.IsPostBack)
            {
                this.GetAdminMenuOptions(this.menu);
                this.GetSubMenuOptions(
                    this.menu,
                    "Orders",
                    this.subMenu,
                    new string[] { "GiveOrder", "TakeOrder" });
            }

            this.OrderGrid.NeedDataSource += OrderGrid_NeedDataSource;
        }

        private void OrderGrid_NeedDataSource(object sender, EventArgs e)
        {
            this.OrderGrid.DataSource = this.ViewModel;
        }
    }
}