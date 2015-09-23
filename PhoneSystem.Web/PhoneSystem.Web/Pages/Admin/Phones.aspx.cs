namespace PhoneSystem.Web.Pages.Admin
{
    using System;
    using System.Linq;
    using System.Web.UI.WebControls;
    using System.Web.ModelBinding;

    using PhoneSystem.Models;
    using PhoneSystem.Web.Presenters.Admin;
    using PhoneSystem.Web.Controls;
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

                this.PhoneGrid.GetData(this.ViewModel);
                this.PhoneGrid.DataBind();
            }

            this.PhoneGrid.GridButtons = GridButtons.Crud;
            this.PhoneGrid.OnBtnCreateClicked += PhoneGrid_OnBtnCreateClicked;
            this.PhoneGrid.OnBtnDeleteClicked += PhoneGrid_OnBtnDeleteClicked;
            this.PhoneGrid.OnBtnEditClicked += PhoneGrid_OnBtnEditClicked;
            this.PhoneGrid.OnBtnViewClicked += PhoneGrid_OnBtnViewClicked;
        }

        void PhoneGrid_OnBtnViewClicked(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);
            var model = new PhoneDetailViewModel();
            this.GetById(id, ref model);
            this.FormCreaterView.CreateForm(model);
        }

        void PhoneGrid_OnBtnEditClicked(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);
            var model = new PhoneEditViewModel();
            this.GetById(id, ref model);
            this.FormCreaterEdit.CreateForm(model);
        }

        void PhoneGrid_OnBtnDeleteClicked(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);
            var model = new PhoneDetailViewModel();
            this.GetById(id, ref model);
            this.FormCreaterDelete.CreateForm(model);
        }

        private void PhoneGrid_OnBtnCreateClicked(object sender, EventArgs e)
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
                this.ReBindGrid();
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
                this.ReBindGrid();
            }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {

        }

        private void ReBindGrid()
        {
            this.TakeViewModel(this.Presenter.GetResult());
            this.PhoneGrid.GetData(this.ViewModel);
            this.PhoneGrid.DataBind();
        }
    }
}