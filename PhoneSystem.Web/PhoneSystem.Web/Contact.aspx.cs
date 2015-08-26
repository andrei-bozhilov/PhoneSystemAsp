namespace PhoneSystem.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var list = new List<string>() { "asdasd" };
            this.Session.Add("Noty-success", list);
            this.Session.Add("Noty-error", list);
        }

        protected void btnSearchSubmit_Click(object sender, EventArgs e)
        {

        }
    }
}