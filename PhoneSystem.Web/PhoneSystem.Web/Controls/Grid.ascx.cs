namespace PhoneSystem.Web.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Reflection;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using PhoneSystem.Common.Extensions;
    using System.Collections;
    using Helpers;

    public enum GridButtons
    {
        None,
        Crud,
        View
    }

    public partial class Grid : UserControl
    {
        #region Properties for placeholders

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateInstance(TemplateInstance.Single)]
        public ITemplate CreateBodyTemplate { get; set; }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateInstance(TemplateInstance.Single)]
        public ITemplate CreateHeaderTemplate { get; set; }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateInstance(TemplateInstance.Single)]
        public ITemplate CreateFooterTemplate { get; set; }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateInstance(TemplateInstance.Single)]
        public ITemplate ViewBodyTemplate { get; set; }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateInstance(TemplateInstance.Single)]
        public ITemplate ViewHeaderTemplate { get; set; }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateInstance(TemplateInstance.Single)]
        public ITemplate ViewFooterTemplate { get; set; }


        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateInstance(TemplateInstance.Single)]
        public ITemplate EditBodyTemplate { get; set; }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateInstance(TemplateInstance.Single)]
        public ITemplate EditHeaderTemplate { get; set; }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateInstance(TemplateInstance.Single)]
        public ITemplate EditFooterTemplate { get; set; }


        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateInstance(TemplateInstance.Single)]
        public ITemplate DeleteBodyTemplate { get; set; }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateInstance(TemplateInstance.Single)]
        public ITemplate DeleteHeaderTemplate { get; set; }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateInstance(TemplateInstance.Single)]
        public ITemplate DeleteFooterTemplate { get; set; }

        #endregion

        #region Properties

        private bool hasCrudOperations = false;

        private bool showId = true;

        private int pageSize = 10;

        private bool showCreateModel = true;

        private bool showViewModel = true;

        private bool showEditModel = true;

        private bool showDeleteModel = true;



        private GridButtons gridButtons;

        #endregion        

        private string searchQuaryString = null;

        protected IList<string> headers;

        protected Dictionary<string, Type> colTypes;

        protected int numItems;

        protected int numPages;

        protected DataTable data;

        public IQueryable DataSource { get; set; }

        public delegate void ButtonViewClickEventHandler(object sender, EventArgs e);

        public delegate void ButtonEditClickEventHandler(object sender, EventArgs e);

        public delegate void ButtonDeleteClickEventHandler(object sender, EventArgs e);

        public delegate void ButtonCreateClickEventHandler(object sender, EventArgs e);

        public delegate void NeedDataSourceEventHandler(object sender, EventArgs e);

        public event ButtonViewClickEventHandler BtnViewClick;

        public event ButtonEditClickEventHandler BtnEditClick;

        public event ButtonDeleteClickEventHandler BtnDeleteClick;

        public event ButtonCreateClickEventHandler BtnCreateClick;

        public event NeedDataSourceEventHandler NeedDataSource;

        #region Options
        public bool HasCrudButtons
        {
            get { return this.hasCrudOperations; }
            set { this.hasCrudOperations = value; }
        }

        public bool ShowId
        {
            get { return this.showId; }
            set { this.showId = value; }
        }

        public int PageSize
        {
            get { return this.pageSize; }
            set { this.pageSize = value; }
        }

        public bool ShowCreateModalBox
        {
            get { return this.showCreateModel; }
            set { this.showCreateModel = value; }
        }

        public bool ShowViewModalBox
        {
            get { return this.showViewModel; }
            set { this.showViewModel = value; }
        }

        public bool ShowEditModalBox
        {
            get { return this.showEditModel; }
            set { this.showEditModel = value; }
        }

        public bool ShowDeleteModalBox
        {
            get { return this.showDeleteModel; }
            set { this.showDeleteModel = value; }
        }

        public GridButtons GridButtons
        {
            get { return this.gridButtons; }
            set { this.gridButtons = value; }
        }

        #endregion

        protected int CurrentPage
        {
            get
            {
                int page;
                return int.TryParse(Request.QueryString["page"], out page) ? page : 1;
            }
        }

        protected string CurrentSorting
        {
            get
            {
                var sort = this.Request.QueryString["sort"];
                return sort == null ? string.Empty : "&sort=" + sort;
            }
        }

        protected string CurrentFiltering
        {
            get
            {
                var sort = this.Request.QueryString["search"];
                return sort == null ? string.Empty : "&search=" + sort;
            }
        }

        protected string BaseUrl
        {
            get
            {
                return this.Request.Url.AbsolutePath;
            }
        }

        protected string Prefix
        {
            get
            {
                return this.ID + "_";
            }
        }

        protected Dictionary<string, string> SortDir { get; set; }

        protected void Page_Init()
        {
            if (Request.QueryString["page"] == null)
            {
                Response.Redirect(this.Request.Url.AbsolutePath + "?page=" + 1);
            }

            if (this.CreateBodyTemplate != null)
            {
                this.CreateBodyTemplate.InstantiateIn(this.PlaceHolderCreateBody);
            }

            if (this.CreateHeaderTemplate != null)
            {
                this.CreateHeaderTemplate.InstantiateIn(this.PlaceHolderCreateHeader);
            }

            if (this.CreateFooterTemplate != null)
            {
                this.CreateFooterTemplate.InstantiateIn(PlaceHolderCreateFooter);
            }


            if (this.ViewBodyTemplate != null)
            {
                this.ViewBodyTemplate.InstantiateIn(this.PlaceHolderViewBody);
            }

            if (this.ViewHeaderTemplate != null)
            {
                this.ViewHeaderTemplate.InstantiateIn(this.PlaceHolderViewHeader);
            }

            if (this.ViewFooterTemplate != null)
            {
                this.ViewFooterTemplate.InstantiateIn(PlaceHolderViewFooter);
            }

            if (this.EditBodyTemplate != null)
            {
                this.EditBodyTemplate.InstantiateIn(PlaceHolderEditBody);
            }

            if (this.EditHeaderTemplate != null)
            {
                this.EditHeaderTemplate.InstantiateIn(PlaceHolderEditHeader);
            }

            if (this.EditFooterTemplate != null)
            {
                this.EditFooterTemplate.InstantiateIn(PlaceHolderEditFooter);
            }


            if (this.DeleteBodyTemplate != null)
            {
                this.DeleteBodyTemplate.InstantiateIn(PlaceHolderDeleteBody);
            }

            if (this.DeleteHeaderTemplate != null)
            {
                this.DeleteHeaderTemplate.InstantiateIn(PlaceHolderDeleteHeader);
            }

            if (this.DeleteFooterTemplate != null)
            {
                this.DeleteFooterTemplate.InstantiateIn(PlaceHolderDeleteFooter);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.NeedDataSource != null)
                {
                    ReBind();
                }
            }

            this.RegisterStartupScripts();
        }

        public void ReBind()
        {
            this.NeedDataSource(this, EventArgs.Empty);
            this.GetData(this.DataSource);
            this.DataBind();
        }

        public void GetData(IQueryable obj)
        {
            Type type = obj.ElementType;

            this.DataSource = obj;

            this.FormCreaterFilter.ShowModelData = false;
            this.FormCreaterFilter.HasValidation = false;
            this.FormCreaterFilter.Prefix = this.Prefix;
            if (obj.Count() > 0)
            {
                this.FormCreaterFilter.CreateForm(ReflectionHelper.CreateObjectOf(type));
            }

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            this.colTypes = new Dictionary<string, Type>();
            this.headers = new List<string>();
            this.SortDir = new Dictionary<string, string>();

            foreach (var prop in properties)
            {
                this.colTypes.Add(prop.Name, prop.PropertyType);

                if (!this.ShowId)
                {
                    if (prop.Name.ToLower() == "id")
                    {
                        continue;
                    }
                }

                this.headers.Add(prop.Name);
                this.SortDir.Add(prop.Name, "&sort=" + prop.Name + "_asc");
            }

            obj = HandleFiltering(obj);
            obj = HandleSorting(obj);
            obj = HandlePaging(obj);

            this.data = obj.ToDataTable();
            this.RepeaterHeaders.DataSource = this.headers;
            this.RepeaterRows.DataSource = this.data.Rows;
            this.RepeaterEmptyRows.DataSource = new string[this.PageSize - this.data.Rows.Count];
        }

        private IQueryable HandleFiltering(IQueryable obj)
        {
            string queryString = this.searchQuaryString ?? Request.QueryString["search"];
            this.searchQuaryString = null; //make it null so Request.QueryString["search"] to has higher priority

            if (queryString != null)
            {
                var searchStrArr = queryString.Split(',');

                for (int i = 0; i < searchStrArr.Length; i++)
                {
                    string[] searchValues = searchStrArr[i].Split('_');
                    string propStr = searchValues[0];
                    string valueStr = searchValues[1];
                    Type type = this.colTypes[propStr];

                    if (type == typeof(string))
                    {
                        // "+ propStr + "=@0"/" is for enum as string
                        obj = obj.Where(propStr + ".Contains(@0) || " + propStr + "=@0", valueStr);
                    }
                    else if (type == typeof(bool))
                    {
                        obj = obj.Where(propStr + "==" + valueStr);
                    }
                    else if (type == typeof(int))
                    {
                        obj = obj.Where(propStr + ">=" + valueStr);
                    }

                    //TODO have to add more data types

                }
            }

            return obj;
        }

        private IQueryable HandlePaging(IQueryable obj)
        {
            this.numItems = obj.Count();
            this.numPages = (numItems + this.PageSize - 1) / this.PageSize;
            int pageNumber = this.numPages > this.CurrentPage || this.numPages == 0 ? this.CurrentPage : this.numPages;


            obj = obj.Skip(this.PageSize * (pageNumber - 1));
            obj = obj.Take(this.PageSize);
            return obj;
        }

        private IQueryable HandleSorting(IQueryable obj)
        {
            if (Request.QueryString["sort"] != null)
            {
                // id_asc => {"id", "asc"}
                var sortStringArr = Request.QueryString.GetValues("sort").First().Split('_');
                var keyUrl = sortStringArr[0];
                var dir = sortStringArr[1];

                if (dir == "asc")
                {
                    foreach (var key in this.SortDir.Keys.ToList())
                    {
                        this.SortDir[key] = "&sort=" + key + "_asc";
                    }

                    this.SortDir[sortStringArr[0]] = "&sort=" + keyUrl + "_desc";
                }
                else
                {
                    foreach (var key in this.SortDir.Keys.ToList())
                    {
                        this.SortDir[key] = "&sort=" + key + "_asc";
                    }
                }

                obj = obj.OrderBy(keyUrl + " " + dir.ToUpper());
            }

            return obj;
        }

        protected void GridBtnCreate_Click(object sender, EventArgs e)
        {
            if (BtnCreateClick != null)
                BtnCreateClick(sender, e);

            if ((this.CreateBodyTemplate != null || this.CreateFooterTemplate != null || this.CreateHeaderTemplate != null) && this.ShowCreateModalBox)
            {
                this.ScriptPlaceHolder.Text = @"
                <script>
                    $(function(){
                        $('#CreatModal').modal('show');
                    })                       
                </script>";
            }
            else
            {
                this.EmptyScriptPlaceHolderText();
            }
        }

        protected void GridBtnView_Click(object sender, EventArgs e)
        {
            if (BtnViewClick != null)
                BtnViewClick(sender, e);

            var btn = sender as Button;
            if ((this.ViewBodyTemplate != null || this.ViewFooterTemplate != null ||
                this.ViewHeaderTemplate != null) && this.ShowViewModalBox)
            {
                this.ScriptPlaceHolder.Text =
                     @"
                <script>
                    $(function(){
                        $('#ViewModal').modal('show');
                    })                       
                </script>";
            }
            else
            {
                this.EmptyScriptPlaceHolderText();
            }
        }

        protected void GridBtnEdit_Click(object sender, EventArgs e)
        {
            if (BtnEditClick != null)
                BtnEditClick(sender, e);

            if ((this.EditBodyTemplate != null || this.EditFooterTemplate != null ||
                this.EditHeaderTemplate != null) && this.ShowEditModalBox)
            {
                this.ScriptPlaceHolder.Text =
                    @"
                <script>
                    $(function(){
                        $('#EditModal').modal('show');
                    })                       
                </script>";
            }
            else
            {
                this.EmptyScriptPlaceHolderText();
            }
        }

        protected void GridBtnDelete_Click(object sender, EventArgs e)
        {
            if (BtnDeleteClick != null)
                BtnDeleteClick(sender, e);

            if ((this.DeleteBodyTemplate != null || this.DeleteFooterTemplate != null || this.DeleteHeaderTemplate != null) && this.ShowEditModalBox)
            {
                this.ScriptPlaceHolder.Text =
                        @"
                <script>
                    $(function(){
                        $('#DeleteModal').modal('show');
                    })                       
                </script>";
            }
            else
            {
                this.EmptyScriptPlaceHolderText();
            }
        }

        protected string ModifyCurrentUrl(string sort, int page = 0, string text = null)
        {
            return this.Request.Url.AbsolutePath + ModifyCurrentQueryString(sort, page, text);
        }

        protected string ModifyCurrentQueryString(string sort, int page = 0, string text = null)
        {
            if (page == 0)
            {
                page = this.CurrentPage;
            }

            if (text == null)
            {
                text = this.CurrentFiltering;
            }
            // this.searchQuaryString = text.Replace("&search=", ",").Remove(0, 1);

            return "?page=" + page + sort + text;
        }

        public override void DataBind()
        {
            this.EmptyScriptPlaceHolderText();
            base.DataBind();
        }

        private void EmptyScriptPlaceHolderText()
        {
            this.ScriptPlaceHolder.Text = string.Empty;
        }

        protected void RepeaterCols_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (ListItemType.Item == e.Item.ItemType || ListItemType.AlternatingItem == e.Item.ItemType)
            {
                var data = e.Item.DataItem;
                Literal container = (Literal)e.Item.FindControl("ValuePlaceHolder");
                container.Mode = LiteralMode.Encode;

                if (data is bool)
                {
                    container.Mode = LiteralMode.PassThrough;
                    if ((bool)data)
                    {
                        container.Text = "<span class='glyphicon glyphicon-ok-circle' style='color:green; font-size:18px'><span>";
                    }
                    else
                    {
                        container.Text = "<span class='glyphicon glyphicon-remove-circle' style='color:red; font-size:18px'><span>";
                    }
                }
                else if (data is Enum)
                {
                    container.Text = data.ToString().PascalCaseToText();
                }
                else if (data is DateTime)
                {
                    DateTime dataDateTime = (DateTime)data;
                    container.Text = dataDateTime.ToString("dd MMM yyyy @ HH:mm");
                }
                else
                {
                    container.Text = data.ToString().PascalCaseToText();
                }
            }
        }

        protected void GridBtnSearch_Click(object sender, EventArgs e)
        {
            var keys = this.Request.Form.AllKeys
                .Where(k => k != null)
                .Where(k => k.Contains(this.Prefix));
            string search = "";
            List<string> searchParams = new List<string>();

            foreach (var key in keys)
            {
                var keyWithoutPrefix = key.Replace(this.Prefix, "");
                var value = this.Request.Form[key];
                if (string.IsNullOrWhiteSpace(value))
                {
                    continue;
                }
                search += "&search=" + keyWithoutPrefix + "_" + value;
                searchParams.Add(keyWithoutPrefix + "_" + value);
            }
            this.searchQuaryString = string.Join(",", searchParams);
            string queryString = ModifyCurrentQueryString(this.CurrentSorting, this.CurrentPage, search);
            Response.Redirect(this.ModifyCurrentUrl(this.CurrentSorting, this.CurrentPage, queryString));
            //string script = string.Format(@"
            //    $(function(){{
            //        helper.changeLocationClientSide('{0}');
            //    }})", queryString);
            //this.ReBind();
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "changeQueryString", "<script>" + script + "</script>", false);
        }

        protected void GridBtnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect(this.ModifyCurrentUrl(this.CurrentSorting, this.CurrentPage, ""));
        }

        public IEnumerable RepeaterEmptyCols_GetData()
        {
            return new string[this.headers.Count];
        }

        private void RegisterStartupScripts()
        {
            string script = @"      
            $(function() {
                $('.dropdown-menu').click(function(e) {
                    e.stopPropagation();
                })
             });";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "loadEvents", "<script>" + script + "</script>", false);
        }

        public IEnumerable<int> PagesRepeater_GetData()
        {
            int from = (this.CurrentPage - 2 < 1) ? 1 : this.CurrentPage - 2;
            int to = (this.CurrentPage + 2 > this.numPages) ? this.numPages : this.CurrentPage + 2;

            if (from + 4 > to && from + 4 <= this.numPages)
            {
                to = from + 4;
            }

            if (to - 4 < from && to - 4 >= 1)
            {
                from = to - 4;
            }

            int[] pages = new int[to - from + 1];
            for (int i = from; i <= to; i++)
            {
                pages[i - 1] = i;
            }

            return pages;
        }
    }
}