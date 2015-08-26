namespace PhoneSystem.Web.Pages.Admin
{
    using System;
    using System.Web.UI.WebControls;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Newtonsoft.Json;

    using PhoneSystem.Web.Presenters.Admin;
    using PhoneSystem.Web.ViewModels.Admin.TakeOrder;
    using Presenters;
    using Presenters.Results;

    public partial class TakeOrder : AdminBasePage<TakeOrderViewModel, TakeOrderPresenter>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.TakeViewModel(new TakeOrderPresenter());

            if (!IsPostBack)
            {
                this.GetAdminMenuOptions(this.menu);
                this.GetSubMenuOptions(
                    this.menu, "Orders", this.subMenu, new string[] { "GiveOrder", "TakeOrder" });

                this.userNames
                    .Attributes.Add("data-source", JsonConvert.SerializeObject(this.ViewModel.UserNames.ToList()));
                this.phoneNumbers
                    .Attributes.Add("data-source", JsonConvert.SerializeObject(this.ViewModel.PhoneNumbers.ToList()));
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            TakeOrderInfoViewModel infoModel = new TakeOrderInfoViewModel();
            string phoneId = this.phoneNumbers.Value;
            string userName = this.userNames.Value;
            IResult result = this.Presenter.GetInfo(phoneId, userName);
            this.TakeIResult(result, ref infoModel);

            if (result is ErrorResult)
            {
                this.NotyTakeOrder.Update(this);

                this.UserRepeter.DataSource = null;
                this.PhonesRepeter.DataSource = null;
                this.OrderRepeater.DataSource = null;
                this.UserRepeter.DataBind();
                this.PhonesRepeter.DataBind();
                this.OrderRepeater.DataBind();
            }
            else
            {
                this.UserRepeter.DataSource = new UserViewModel[] { infoModel.UserInfo };
                this.PhonesRepeter.DataSource = infoModel.PhonesInfo;
                this.OrderRepeater.DataSource = infoModel.OrdersInfo;

                this.UserRepeter.DataBind();
                this.PhonesRepeter.DataBind();
                this.OrderRepeater.DataBind();
            }
        }

        protected void BtnTakaCard_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string phoneId = btn.CommandArgument;
            IResult result = this.Presenter.TakePhone(phoneId, this.User.Identity.GetUserId());
            this.TakeIResult(result);

            this.NotyTakeOrder.Update(this);

            this.UserRepeter.DataSource = null;
            this.PhonesRepeter.DataSource = null;
            this.OrderRepeater.DataSource = null;
            this.UserRepeter.DataBind();
            this.PhonesRepeter.DataBind();
            this.OrderRepeater.DataBind();
        }
    }
}