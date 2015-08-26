namespace PhoneSystem.Web.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI.WebControls;
    using System.Reflection;

    using PhoneSystem.Web.Helpers;
    using System.Web.UI;

    public partial class AdminMenu : UserControl
    {
        private string currentPage;

        public IEnumerable<string> List { get; set; }

        public IList<string> BlackList { get; set; }

        public Type BaseClassName { get; set; }

        public string FirstElementName { get; set; }

        public string CurrentPageName
        {
            get
            {
                if (this.currentPage == null)
                {
                    return this.currentPage;
                }

                return this.currentPage.ToLower();
            }
            set
            {
                this.currentPage = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IEnumerable<string> items_GetData()
        {

            if (this.List == null)
            {
                if (this.BaseClassName == null)
                {
                    throw new ArgumentNullException("BaseClassName");
                }

                MethodInfo method = typeof(ReflectionHelper).GetMethod("GetSubClasses");
                MethodInfo genericMethod = method.MakeGenericMethod(BaseClassName);
                List<Type> genericList = (List<Type>)genericMethod.Invoke(null, null);

                var list = genericList.Select(x => x.Name).ToList();
                if (this.FirstElementName != null)
                {
                    bool isExistFirstElementName = list.Remove(this.FirstElementName);
                    if (!isExistFirstElementName)
                    {
                        throw new ArgumentException("Must exist page, named " + this.FirstElementName + ".");
                    }
                    list.Insert(0, this.FirstElementName);
                }

                if (this.BlackList != null)
                {
                    foreach (var item in this.BlackList)
                    {
                        list.Remove(item);
                    }
                }

                this.List = list;
            }

            return this.List;
        }
    }
}