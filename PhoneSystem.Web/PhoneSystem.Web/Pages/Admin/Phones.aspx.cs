namespace PhoneSystem.Web.Pages.Admin
{
    using System;
    using System.Linq;
    using System.Web.UI.WebControls;

    using PhoneSystem.Models;
    using PhoneSystem.Web.Presenters.Admin;
    using PhoneSystem.Web.ViewModels.Admin.Phones;

    public partial class Phones : AdminCrudPage<IQueryable<PhoneViewModel>, Phone, PhonePresenter>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.TakeViewModel(new PhonePresenter());
            if (!this.IsPostBack)
            {
                this.GetAdminMenuOptions(this.menu);
                this.GetSubMenuOptions(this.menu, "Tables", this.subMenu,
                    new string[] { "Users", "Phones", "Departments", "JobTitles" });
            }

            this.PhoneGrid.NeedDataSource += PhoneGrid_OnNeedDataSource;
        }

        private void PhoneGrid_OnNeedDataSource(object sender, EventArgs e)
        {
            this.PhoneGrid.DataSource = this.ViewModel;
        }

        protected void PhoneGrid_BtnViewClick(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);
            var model = new PhoneDetailViewModel();
            this.GetById(id, ref model);
            this.FormCreaterView.CreateForm(model);
        }

        protected void PhoneGrid_BtnEditClick(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);
            var model = new PhoneEditViewModel();
            this.GetById(id, ref model);
            this.FormCreaterEdit.CreateForm(model);
        }

        protected void PhoneGrid_BtnDeleteClick(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);
            var model = new PhoneDetailViewModel();
            this.GetById(id, ref model);
            this.FormCreaterDelete.CreateForm(model);
        }

        protected void PhoneGrid_BtnCreateClick(object sender, EventArgs e)
        {
            var model = new PhoneEditViewModel();
            this.FormCreaterCreate.CreateForm(model);
        }

        protected void BtnCreat_Click(object sender, EventArgs e)
        {
            var model = new Phone();
            this.TryUpdateModel(model, this.FormCreaterCreate.Prefix);
            if (this.ModelState.IsValid)
            {
                this.Create(model);
                this.NotyPhone.Update(this);
                this.PhoneGrid.ReBind();
            }
        }

        protected void ButtonEdit_Click(object sender, EventArgs e)
        {
            var model = new Phone();
            this.TryUpdateModel(model, this.FormCreaterEdit.Prefix);
            if (this.ModelState.IsValid)
            {
                this.Edit(model);
                this.NotyPhone.Update(this);
                this.PhoneGrid.ReBind();
            }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}