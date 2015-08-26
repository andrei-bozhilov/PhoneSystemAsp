namespace PhoneSystem.Web.ViewModels.Admin.GiveOrder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI.WebControls;

    public class GiveOrderViewModel
    {
        public IQueryable<ListItem> Departments { get; set; }

        public IQueryable<ListItem> JobTitles { get; set; }

        public IQueryable<ListItem> FreePhoneNumbers { get; set; }

        public IQueryable<string> Users { get; set; }
    }
}