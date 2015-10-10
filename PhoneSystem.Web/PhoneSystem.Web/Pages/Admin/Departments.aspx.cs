namespace PhoneSystem.Web.Pages.Admin
{
    using System;
    using System.Linq;

    using PhoneSystem.Web.Presenters.Admin;
    using PhoneSystem.Models;
    using PhoneSystem.Web.Controls;
    using PhoneSystem.Web.ViewModels.Admin.Departments;

    public partial class Departments :
         AdminCrudPage<IQueryable<DepartmentViewModel>, Department, DepartmentPresenter>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.TakeViewModel(new DepartmentPresenter());

            if (!this.IsPostBack)
            {
                this.GetAdminMenuOptions(this.menu);
                this.GetSubMenuOptions(
                     this.menu,
                     "Tables",
                     this.subMenu,
                     new string[] { "Users", "Phones", "Departments", "JobTitles" });
            }


            this.DepartmentGrid.NeedDataSource += DepartmentGrid_NeedDataSource;
        }

        private void DepartmentGrid_NeedDataSource(object sender, EventArgs e)
        {
            this.DepartmentGrid.DataSource = this.ViewModel;
        }

        protected void DepartmentGrid_BtnViewClick(object sender, EventArgs e)
        {
            int id = int.Parse(this.GetIdFromBtnSender(sender));
            var model = new DepartmentDetailViewModel();
            this.GetById(id, ref model);
            this.FormCreaterView.CreateForm(model);
        }

        protected void DepartmentGrid_BtnCreateClick(object sender, EventArgs e)
        {
            var model = new DepartmentEditViewModel();
            this.FormCreaterCreate.CreateForm(model);
        }

        protected void DepartmentGrid_BtnEditClick(object sender, EventArgs e)
        {
            int id = int.Parse(this.GetIdFromBtnSender(sender));
            var model = new DepartmentEditViewModel();
            this.GetById(id, ref model);
            this.FormCreaterEdit.CreateForm(model);
        }

        protected void DepartmentGrid_BtnDeleteClick(object sender, EventArgs e)
        {
            int id = int.Parse(this.GetIdFromBtnSender(sender));
            var model = new DepartmentDetailViewModel();
            this.GetById(id, ref model);
            this.FormCreaterDelete.CreateForm(model);
        }

        protected void BtnCreate_Click(object sender, EventArgs e)
        {
            var model = new Department();
            this.TryUpdateModel(model, this.FormCreaterCreate.Prefix);
            if (this.ModelState.IsValid)
            {
                this.Create(model);
                this.ReBindGrid();
                NotyDepartments.Update(this);
            }
        }

        protected void ButtonEdit_Click(object sender, EventArgs e)
        {
            var model = new Department();
            this.TryUpdateModel(model, this.FormCreaterEdit.Prefix);
            if (this.ModelState.IsValid)
            {
                this.Edit(model);
                this.ReBindGrid();
                NotyDepartments.Update(this);
            }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(this.Request.Form["Deleteid"]);
            if (this.CheckBoxShowDeleted.Checked)
            {
                this.UnDelete(id);
                this.ReBindGrid();
                NotyDepartments.Update(this);
            }
            else
            {
                this.Delete(id);
                this.ReBindGrid();
                NotyDepartments.Update(this);
            }
        }

        protected void CheckBoxShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            this.ReBindGrid();
        }

        private void ReBindGrid()
        {
            if (this.CheckBoxShowDeleted.Checked)
            {
                this.TakeViewModel(this.Presenter.DataItemDeleted());

            }
            else
            {
                this.TakeViewModel(this.Presenter.GetResult());
            }

            this.DepartmentGrid.ReBind();
        }
    }
}