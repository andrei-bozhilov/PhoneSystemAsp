namespace PhoneSystem.Web.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI;
    using PhoneSystem.Web.Helpers;

    public partial class Noty : UserControl
    {
        private IEnumerable<string> SuccesCollection { get; set; }

        private IEnumerable<string> ErrorCollection { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Request.IsAjaxRequest())
            {
                this.Update(this.Page);
            }
        }

        public IEnumerable<string> NotySuccess_GetData()
        {
            return this.SuccesCollection;
        }

        public IEnumerable<string> NotyError_GetData()
        {
            return this.ErrorCollection;
        }

        public void Update(Page page)
        {
            this.SuccesCollection = this.Context.Session["noty-success"] as IEnumerable<string>;
            this.ErrorCollection = this.Context.Session["noty-error"] as IEnumerable<string>;

            if (this.SuccesCollection != null)
            {
                string successScript = @"$(document).ready(function() {{
                                            $('#success-modal-{0}').modal('show')
                                      }}); ";
                var success = string.Format(successScript, this.ClientID);
                ScriptManager.RegisterStartupScript(this, Page.GetType(), Guid.NewGuid().ToString(), success, true);
            }

            if (this.ErrorCollection != null)
            {
                string errorScript = @"$(document).ready(function() {{
                                        $('#error-modal-{0}').modal('show');
                                    }}); ";

                var error = string.Format(errorScript, this.ClientID);
                ScriptManager.RegisterStartupScript(this, Page.GetType(), Guid.NewGuid().ToString(), error, true);
            }

            this.NotySuccess.DataSource = this.SuccesCollection;
            this.NotySuccess.DataBind();

            this.NotyError.DataSource = this.ErrorCollection;
            this.NotyError.DataBind();

            this.Context.Session.Remove("noty-success");
            this.Context.Session.Remove("noty-error");
        }
    }
}