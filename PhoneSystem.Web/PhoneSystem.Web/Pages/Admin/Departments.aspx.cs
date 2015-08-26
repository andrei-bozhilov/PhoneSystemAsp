namespace PhoneSystem.Web.Pages.Admin
{
    using System;
    using System.Linq;
    using System.Web.ModelBinding;

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

                this.DepartmentGrid.GetData(this.ViewModel);
                this.DepartmentGrid.DataBind();
            }

            this.DepartmentGrid.GridButtons = GridButtons.Crud;
            this.DepartmentGrid.OnBtnCreateClicked += DepartmentGrid_OnBtnCreateClicked;
            this.DepartmentGrid.OnBtnDeleteClicked += DepartmentGrid_OnBtnDeleteClicked;
            this.DepartmentGrid.OnBtnEditClicked += DepartmentGrid_OnBtnEditClicked;
            this.DepartmentGrid.OnBtnViewClicked += DepartmentGrid_OnBtnViewClicked;
        }

        void DepartmentGrid_OnBtnCreateClicked(object sender, EventArgs e)
        {
            var model = new DepartmentEditViewModel();
            this.FormCreaterCreate.CreateForm(model);
        }

        void DepartmentGrid_OnBtnViewClicked(object sender, EventArgs e)
        {
            int id = int.Parse(this.GetIdFromBtnSender(sender));
            var model = new DepartmentDetailViewModel();
            this.GetById(id, ref model);
            this.FormCreaterView.CreateForm(model);
        }

        void DepartmentGrid_OnBtnEditClicked(object sender, EventArgs e)
        {
            int id = int.Parse(this.GetIdFromBtnSender(sender));
            var model = new DepartmentEditViewModel();
            this.GetById(id, ref model);
            this.FormCreaterEdit.CreateForm(model);
        }

        void DepartmentGrid_OnBtnDeleteClicked(object sender, EventArgs e)
        {
            int id = int.Parse(this.GetIdFromBtnSender(sender));
            var model = new DepartmentDetailViewModel();
            this.GetById(id, ref model);
            this.FormCreaterDelete.CreateForm(model);
        }

        protected void BtnCreate_Click(object sender, EventArgs e)
        {
            var model = new Department();
            this.TryUpdateModel(model, new FormValueProvider(this.ModelBindingExecutionContext));
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
            this.TryUpdateModel(model, new FormValueProvider(this.ModelBindingExecutionContext));
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

            this.DepartmentGrid.GetData(this.ViewModel);
            this.DepartmentGrid.DataBind();
        }
    }
}