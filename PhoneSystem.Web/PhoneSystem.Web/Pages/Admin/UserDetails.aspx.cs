namespace PhoneSystem.Web.Pages.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using PhoneSystem.Web.ViewModels.Admin;
    using PhoneSystem.Models;
    using PhoneSystem.Web.Presenters.Admin;
    using System.Web.ModelBinding;
    using PhoneSystem.Web.Controls;
    using PhoneSystem.Web.ViewModels.Admin.Orders;
    using PhoneSystem.Web.ViewModels.Admin.UserDetails;

    public partial class UserDetails : AdminCrudPage<UserDetailInfoViewModel, UserInfoViewModel, UserDetailsPresenter>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string userId = (string)this.RouteData.Values["params"];
            this.TakeViewModel(new UserDetailsPresenter(userId));

            if (!this.IsPostBack)
            {
                this.GetAdminMenuOptions(this.menu);
                this.GetSubMenuOptions(this.menu,
                    "Tables",
                    this.subMenu,
                    new string[] { "Users", "Phones", "Departments", "JobTitles" },
                    "Users");
            }

            this.FormCreaterUser.CreateForm(this.ViewModel.UserInfo);
            this.CheckBoxIsAdmin.Checked = this.ViewModel.UserInfo.IsAdmin;
            this.RepeaterPhones.DataSource = this.ViewModel.PhonesInfo.ToList();
            this.RepeaterPhones.DataBind();
            this.RepeaterOrders.DataSource = this.ViewModel.OrdersInfo.ToList();
            this.RepeaterOrders.DataBind();
        }

        protected void BtnChangeUser_Click(object sender, EventArgs e)
        {
            UserInfoViewModel user = new UserInfoViewModel();
            TryUpdateModel(user, new FormValueProvider(this.ModelBindingExecutionContext));
            user.DepartmentName = this.Request.Form["DepartmentName"];
            user.JobTitleName = this.Request.Form["JobTitleName"];
            if (user.DepartmentName == null || user.DepartmentName == "0")
            {
                this.ModelState.AddModelError("Error", "Department must not be default value");
            }

            if (user.JobTitleName == null || user.JobTitleName == "0")
            {
                this.ModelState.AddModelError("Error", "Job title must not be default value");
            }

            if (this.ModelState.IsValid)
            {
                this.Edit(user);
            }
        }

        protected void CheckBoxIsAdmin_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = sender as CheckBox;
            bool isCheched = checkbox.Checked;
            string userId = (string)this.RouteData.Values["params"];
            this.TakeIResult(this.Presenter.ModifyUserToRoleAdmin(userId, isCheched, this.Context));
        }

        protected void RepeaterPhones_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (ListItemType.Item == e.Item.ItemType || ListItemType.AlternatingItem == e.Item.ItemType)
            {
                FormCreater from = (FormCreater)e.Item.FindControl("FormCreaterPhones");
                from.CreateForm((PhoneInfoViewModel)e.Item.DataItem);
            }
        }

        protected void RepeaterOrders_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (ListItemType.Item == e.Item.ItemType || ListItemType.AlternatingItem == e.Item.ItemType)
            {
                FormCreater from = (FormCreater)e.Item.FindControl("FormCreaterOrders");
                from.CreateForm((OrdersViewModel)e.Item.DataItem);
            }
        }
    }
}