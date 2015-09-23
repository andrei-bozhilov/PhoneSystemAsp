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
            if (!this.IsPostBack)
            {
                this.GetAdminMenuOptions(this.menu);
                this.GetSubMenuOptions(
                    this.menu,
                    "Orders",
                    this.subMenu,
                    new string[] { "GiveOrder", "TakeOrder" });

                this.TakeViewModel(new OrderPresenter());
                this.OrderGrid.GetData(this.ViewModel);
                this.OrderGrid.DataBind();
            }
        }
    }
}