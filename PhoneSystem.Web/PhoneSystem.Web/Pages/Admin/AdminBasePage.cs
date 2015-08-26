namespace PhoneSystem.Web.Pages.Admin
{
    using System.Collections.Generic;

    using PhoneSystem.Web.Controls;
    using Presenters;

    public abstract class AdminBasePage<VM, P> : BasePage<VM, P> where P : IPresenter<VM>
    {
        protected void GetAdminMenuOptions(AdminMenu menu)
        {
            menu.CurrentPageName = this.PageName;
            menu.FirstElementName = "Home";
            // menu.BaseClassName = typeof(IBasePage);
            menu.List = new List<string> { "Home", "Orders", "Tables", "Phonebook" };
            menu.BlackList =
                new List<string>()
                {
                    "GiveOrder", "TakeOrder", "JobTitles",
                    "Users", "Departments", "Phones"
                };
        }

        protected void GetSubMenuOptions(AdminMenu menu, string MainPageName, AdminMenu subMenu, IEnumerable<string> subMenuList, string currentPageName = null)
        {
            menu.CurrentPageName = MainPageName;
            subMenu.List = subMenuList;
            if (currentPageName == null)
            {
                subMenu.CurrentPageName = this.PageName;
            }
            else
            {
                subMenu.CurrentPageName = currentPageName;
            }
        }
    }
}