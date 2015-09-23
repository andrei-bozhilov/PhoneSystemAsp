namespace PhoneSystem.Web.Pages.Admin
{
    using System;
    using System.Linq;

    using PhoneSystem.Web.Presenters.Admin;
    using PhoneSystem.Models;
    using PhoneSystem.Web.Controls;
    using PhoneSystem.Web.ViewModels.Admin.JobTitles;

    public partial class JobTitles : AdminCrudPage<IQueryable<JobTitleViewModel>, JobTitle, JobTitlePresenter>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            this.TakeViewModel(new JobTitlePresenter());

            if (!this.IsPostBack)
            {
                this.GetAdminMenuOptions(this.menu);
                this.GetSubMenuOptions(this.menu, "Tables", this.subMenu,
                     new string[] { "Users", "Phones", "Departments", "JobTitles" });

                this.JobTitleGrid.GetData(this.ViewModel);
                this.JobTitleGrid.DataBind();
            }

            this.JobTitleGrid.GridButtons = GridButtons.Crud;
            this.JobTitleGrid.OnBtnViewClicked += JobTitleGrid_OnBtnViewClicked;
            this.JobTitleGrid.OnBtnEditClicked += JobTitleGrid_OnBtnEditClicked;
            this.JobTitleGrid.OnBtnDeleteClicked += JobTitleGrid_OnBtnDeleteClicked;
            this.JobTitleGrid.OnBtnCreateClicked += JobTitleGrid_OnBtnCreateClicked;
        }

        protected void JobTitleGrid_OnBtnCreateClicked(object sender, EventArgs e)
        {
            var model = new JobTitleEditViewModel();
            this.FormCreaterCreate.CreateForm(model);
        }

        protected void JobTitleGrid_OnBtnViewClicked(object sender, EventArgs e)
        {
            int jobTitleId = int.Parse(GetIdFromBtnSender(sender));
            var jobTitle = new JobTitleDetailViewModel();
            this.GetById(jobTitleId, ref jobTitle);
            this.FormCreaterView.CreateForm(jobTitle);
        }

        protected void JobTitleGrid_OnBtnEditClicked(object sender, EventArgs e)
        {
            int jobTitleId = int.Parse(GetIdFromBtnSender(sender));
            var jobTitle = new JobTitleEditViewModel();
            this.GetById(jobTitleId, ref jobTitle);
            this.FormCreaterEdit.CreateForm(jobTitle);
        }

        void JobTitleGrid_OnBtnDeleteClicked(object sender, EventArgs e)
        {
            int jobTitleId = int.Parse(GetIdFromBtnSender(sender));
            var jobTitle = new JobTitleDetailViewModel();
            this.GetById(jobTitleId, ref jobTitle);
            this.FormCreaterDelete.CreateForm(jobTitle);
        }

        protected void BtnCreate_Click(object sender, EventArgs e)
        {
            var model = new JobTitle();
            this.TryUpdateModel(model, this.FormCreaterCreate.Prefix);
            if (this.ModelState.IsValid)
            {
                this.Create(model);
                NotyJobTitle.Update(this);
                this.ReBindGrid();
            }
        }

        protected void ButtonEdit_Click(object sender, EventArgs e)
        {
            var model = new JobTitle();
            TryUpdateModel(model, this.FormCreaterEdit.Prefix);
            if (this.ModelState.IsValid)
            {
                this.Edit(model);
                NotyJobTitle.Update(this);
                this.ReBindGrid();
            }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            int jobTitleId = int.Parse(this.Request.Form["Deleteid"]);
            if (this.CheckBoxShowDeleted.Checked)
            {
                this.UnDelete(jobTitleId);
                NotyJobTitle.Update(this);
                this.ReBindGrid();
            }
            else
            {
                this.Delete(jobTitleId);
                NotyJobTitle.Update(this);
                this.ReBindGrid();
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
            this.JobTitleGrid.GetData(this.ViewModel);
            this.JobTitleGrid.DataBind();
        }
    }
}