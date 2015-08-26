using PhoneSystem.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhoneSystem.Web
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.GetOwinContext().Authentication.User.Identity.IsAuthenticated)
            {
                if (this.Request.GetOwinContext().Authentication.User.IsInRole(GlobalConstants.AdminRole))
                {
                    this.Response.Redirect("/Admin/Home");
                }
                else
                {
                    this.Response.Redirect("/User/Home");
                }
            }
        }
    }
}