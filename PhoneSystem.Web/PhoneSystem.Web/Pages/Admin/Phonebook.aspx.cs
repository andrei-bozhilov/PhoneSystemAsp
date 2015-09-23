namespace PhoneSystem.Web.Pages.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Newtonsoft.Json;

    using PhoneSystem.Web.Presenters.Admin;
    using PhoneSystem.Web.ViewModels.Admin.Phonebook;

    public partial class Phonebook : AdminBasePage<IQueryable<PhonebookViewModel>, PhonebookPresenter>
    {
        protected bool IsGroupByPhone { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.TakeViewModel(new PhonebookPresenter());
            if (!this.IsPostBack)
            {
                this.GetAdminMenuOptions(this.menu);
            }

            this.IsGroupByPhone = false;

            if (this.Request.QueryString["groupBy"] == null)
            {
                this.Redirect(this.Request.Url + "?groupBy=name");
            }

            this.phoneNumber.Attributes.Add("data-source",
                JsonConvert.SerializeObject(this.ViewModel.Select(x => x.PhoneNumber)));

            this.GetData();
            this.PhoneSearch.Visible = this.IsGroupByPhone;
            this.PhoneSearch.DataBind();
        }

        protected void GetData()
        {
            string groupByParam = this.Request.QueryString["groupBy"] ?? "Name";
            List<string> dataSourceLetters = null;
            this.TakeIResult(this.Presenter.GetLetters(groupByParam), ref dataSourceLetters);

            string getParam = this.Request.QueryString["get"] ?? dataSourceLetters.FirstOrDefault();

            Dictionary<string, List<PhonebookViewModel>> dataSourceGroupBy = null;
            this.TakeIResult(this.Presenter.GetGroupByParameterData(groupByParam, getParam), ref dataSourceGroupBy);

            if (groupByParam == "phone")
            {
                this.IsGroupByPhone = true;
            }

            this.RepeaterByLetters.DataSource = dataSourceLetters;
            this.RepeaterByLetters.DataBind();

            this.RepeaterByGroup.DataSource = dataSourceGroupBy;
            this.RepeaterByGroup.DataBind();

            ScriptManager.RegisterStartupScript(this.UpdatePanelPhonebook, this.GetType(), "PhonebookScript", "app.GetPhonebookFunction()", true);
        }

        protected void RepeaterByGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (this.RepeaterByGroup.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    Control messagePanel = e.Item.FindControl("MessageNoData");
                    messagePanel.Visible = true;
                }
            }
        }
    }
}