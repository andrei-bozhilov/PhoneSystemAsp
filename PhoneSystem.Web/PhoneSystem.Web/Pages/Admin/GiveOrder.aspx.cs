namespace PhoneSystem.Web.Pages.Admin
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.ModelBinding;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Newtonsoft.Json;

    using PhoneSystem.Models;
    using PhoneSystem.Web.Presenters.Admin;
    using PhoneSystem.Web.ViewModels.Admin;
    using PhoneSystem.Web.BindModels.Admin;
    using Presenters;
    using Presenters.Results;
    using PhoneSystem.Web.ViewModels.Admin.GiveOrder;

    public partial class GiveOrder : AdminBasePage<GiveOrderViewModel, GiveOrderPresenter>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.TakeViewModel(new GiveOrderPresenter());

            if (!this.IsPostBack)
            {
                this.GetAdminMenuOptions(this.menu);
                this.GetSubMenuOptions(
                    this.menu,
                    "Orders",
                    this.subMenu,
                    new string[] { "GiveOrder", "TakeOrder" });

                this.DropDownListDepartments.DataSource = this.ViewModel.Departments.ToList();
                this.DropDownListJobTitle.DataSource = this.ViewModel.JobTitles.ToList();
                this.DropDownListPhone.DataSource = this.ViewModel.FreePhoneNumbers.ToList();
                this.DropDownListPhoneOldUser.DataSource = this.DropDownListPhone.DataSource;
                this.OldUser.Attributes.Add("data-source", JsonConvert.SerializeObject(this.ViewModel.Users.ToList()));

                this.DataBind();
            }
        }

        protected void btnCreateNewUser_ServerClick(object sender, EventArgs e)
        {
            var insertModel = new GiveOrderBindingModel();
            TryUpdateModel(insertModel, new FormValueProvider(ModelBindingExecutionContext));

            if (this.DropDownListPhone.SelectedValue == "0")
            {
                this.ModelState.AddModelError("error", "Please select number");
            }

            if (this.DropDownListJobTitle.SelectedValue == "0")
            {
                this.ModelState.AddModelError("error", "Please select job title");
            }

            if (this.DropDownListDepartments.SelectedValue == "0")
            {
                this.ModelState.AddModelError("error", "Please select department");
            }

            if (this.ModelState.IsValid)
            {
                insertModel.DepartmentId = int.Parse(this.DropDownListDepartments.SelectedValue);
                insertModel.JobTitleId = int.Parse(this.DropDownListJobTitle.SelectedValue);
                insertModel.PhoneId = int.Parse(this.DropDownListPhone.SelectedValue);

                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();

                IResult result = this.Presenter.CreateUser(insertModel, manager, signInManager);
                User user = null;
                this.TakeIResult(result, ref user);

                if (result is DataResult<User>)
                {
                    this.DropDownListDepartments.SelectedValue = "0";
                    this.DropDownListJobTitle.SelectedValue = "0";
                    this.DropDownListPhone.SelectedValue = "0";

                    IResult createOrderResult =
                        this.Presenter.CreatePhoneOrder(this.User.Identity.GetUserId(), user.Id, insertModel.PhoneId);
                    this.TakeIResult(createOrderResult);
                }
            }
            else
            {
                this.ShowErrorsInModelState();
            }

            NotyFirstPanel.Update(this);
        }

        protected void BtnShowUserInfo_Click(object sender, EventArgs e)
        {
            string username = this.OldUser.Value;
            IResult result = this.Presenter.GetUserData(username);
            UserViewModel user = new UserViewModel();
            this.TakeIResult(result, ref user);

            this.InputDepartment.Value = user.DepartmentName;
            this.InputJobTitle.Value = user.JobTitleName;
        }

        protected void BtnOldUser_ServerClick(object sender, EventArgs e)
        {
            if (this.DropDownListPhoneOldUser.SelectedValue == "0")
            {
                this.ModelState.AddModelError("error", "Please select number.");
            }

            if (this.ModelState.IsValid)
            {
                string selectedUsername = this.OldUser.Value;
                string currentUserId = this.User.Identity.GetUserId();
                int phoneId = int.Parse(this.DropDownListPhoneOldUser.SelectedValue);

                IResult result =
                    this.Presenter.CreatOrderFromOldUser(selectedUsername, currentUserId, phoneId);
                this.TakeIResult(result);
            }
            else
            {
                this.ShowErrorsInModelState();
            }

            NotySecondPanel.Update(this);
        }
    }
}