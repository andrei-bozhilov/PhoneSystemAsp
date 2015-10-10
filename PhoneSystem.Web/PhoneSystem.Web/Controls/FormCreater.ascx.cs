namespace PhoneSystem.Web.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using PhoneSystem.Common.Extensions;
    using PhoneSystem.Web.Controls.Attibutes;
    using System.Collections;
    using PhoneSystem.Web.ViewModels;
    using System.Text;

    public partial class FormCreater : UserControl
    {
        private bool showModelData = true;
        private bool hasValidation = true;
        private string prefix = string.Empty;

        public bool ShowModelData
        {
            get { return this.showModelData; }
            set { this.showModelData = value; }
        }

        public bool HasValidation
        {
            get { return this.hasValidation; }
            set { this.hasValidation = value; }
        }

        public string Prefix
        {
            get { return this.prefix; }
            set { this.prefix = value; }
        }

        public IList<ObjectProperty> Properties { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CreateForm<T>(T formModel)
        {
            Type type = formModel.GetType();
            this.CreateForm(formModel, type);
        }

        public void CreateForm(object formModel, Type type)
        {
            this.Properties = GetProperties(formModel, type);
            this.RepeaterObject.DataSource = this.Properties;
            this.RepeaterObject.DataBind();
        }

        private IList<ObjectProperty> GetProperties(object formData, Type typeData)
        {
            IList<ObjectProperty> properties = new List<ObjectProperty>();
            PropertyInfo[] propInfos =
                typeData.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in propInfos)
            {
                Type propType = prop.PropertyType;
                string propName = prop.Name;

                object value = null;
                if (this.showModelData)
                {
                    value = prop.GetValue(formData);
                }

                var notMapped = prop.GetCustomAttribute<NotMapPropertyAttribute>();

                if (notMapped != null)
                {
                    continue;
                }

                bool canBeModified = ExecuteModifiedAttribute(prop);
                bool hasSelectCollectionAtt = ExecuteSelectCollectionAttibute(properties, prop, propType, value, canBeModified);

                if (hasSelectCollectionAtt)
                {
                    continue;
                }

                propType = ExecuteEnumAttribute(prop, propType);

                properties.Add(new ObjectProperty(propName, propType, value, canBeModified));
            }

            return properties;
        }

        private static Type ExecuteEnumAttribute(PropertyInfo prop, Type propType)
        {
            EnumCollectionAttribute enumAttr = prop.GetCustomAttribute<EnumCollectionAttribute>();

            if (enumAttr != null)
            {
                propType = enumAttr.EnumName;
            }
            return propType;
        }

        private static bool ExecuteSelectCollectionAttibute(IList<ObjectProperty> properties, PropertyInfo prop, Type propType, object value, bool canBeModified)
        {
            var selectAttribute = prop.GetCustomAttribute<SelectCollectionAttribute>();

            if (selectAttribute != null)
            {
                var selectProp = properties.FirstOrDefault(x => x.PropertyName == selectAttribute.SelectFor);
                if (selectProp == null)
                {
                    throw new ArgumentException("SelectAttribute property SelectFor must match existing property on the object and must be after it.");
                }

                int index = properties.IndexOf(selectProp);
                properties[index] = new ObjectProperty(selectProp.PropertyName, propType, value, canBeModified);
                properties[index].Selected = selectProp.Value;
                properties[index].SelectDefaultValue = selectAttribute.SelectDefaultValue;
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool ExecuteModifiedAttribute(PropertyInfo prop)
        {
            var modifiedAttribute = prop.GetCustomAttribute<CanBeModifiedAttribute>();

            if (modifiedAttribute != null)
            {
                return modifiedAttribute.Modified;
            }
            else
            {
                return true;
            }
        }

        protected void RepeaterObject_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (ListItemType.Item == e.Item.ItemType || ListItemType.AlternatingItem == e.Item.ItemType)
            {
                ObjectProperty item = (ObjectProperty)e.Item.DataItem;
                Literal container = (Literal)e.Item.FindControl("ValuePlaceHolder");

                if (item.Value == null && item.PropertyType.BaseType != typeof(Enum))
                {
                    string text = string.Empty;

                    if (item.CanBeModified)
                    {
                        text = "<input type='text' id='{0}' name='{0}' class='form-control'  value='{1}' />";
                    }
                    else
                    {
                        text = "<input type='text' id='{0}' name='{0}' class='form-control' readonly='readonly'  value='{1}' />";
                    }

                    container.Text = string.Format(text,
                        this.Prefix + item.PropertyName,
                         item.Value);
                }
                else if (item.PropertyType.Name == "IEnumerable`1") // check is it IEnumerable
                {
                    StringBuilder text = new StringBuilder();
                    if (item.Value != null)
                    {
                        var collection = item.Value as IEnumerable;
                        text.AppendFormat("<select name='{0}' class='form-control'>", this.Prefix + item.PropertyName);
                        text.AppendFormat("<option value='0'>{0}</option>", item.SelectDefaultValue);
                        foreach (KeyValuePair element in collection)
                        {
                            if ((string)item.Selected == element.Value)
                            {
                                text.AppendFormat("<option selected value='{0}'>{1}</option>",
                                element.Key, HttpUtility.HtmlEncode(element.Value));
                            }
                            else
                            {
                                text.AppendFormat("<option value='{0}'>{1}</option>",
                              element.Key, HttpUtility.HtmlEncode(element.Value));
                            }
                        }

                        text.Append("</select>");
                    }
                    container.Text = text.ToString();
                }
                else if (item.PropertyType.BaseType == typeof(Enum))
                {
                    ProcessEnumType(item, container, this.Prefix);
                }
                //else if (item.PropertyType == typeof(bool))
                //{
                //    if ((bool)item.Value)
                //    {
                //        container.Text = "<span class='glyphicon glyphicon-ok-circle' style='color:green; font-size:18px'><span>";
                //    }
                //    else
                //    {
                //        container.Text = "<span class='glyphicon glyphicon-remove-circle' style='color:red; font-size:18px'><span>";
                //    }
                //}
                else
                {
                    string text = string.Empty;

                    if (item.CanBeModified)
                    {
                        text = "<input type='text' id='{0}' name='{0}' class='form-control'  value='{1}' />";
                    }
                    else
                    {
                        text = "<input type='text' id='{0}' name='{0}' class='form-control' readonly='readonly'  value='{1}' />";
                    }

                    container.Text = string.Format(text,
                        this.Prefix + item.PropertyName,
                         HttpUtility.HtmlEncode(item.Value));
                }
            }
        }

        private static void ProcessEnumType(ObjectProperty item, Literal container, string prefix)
        {
            var enumNames = Enum.GetNames(item.PropertyType);
            StringBuilder text = new StringBuilder();

            text.AppendFormat("<select name='{0}' class='form-control'>", prefix +
                 item.PropertyName);

            if (item.Value == null)
            {
                text.AppendFormat("<option selected value=' '>Select {0}</option>",
                    item.PropertyName.PascalCaseToText());

                foreach (var enumName in enumNames)
                {
                    text.AppendFormat("<option value='{0}'>{1}</option>",
                  enumName, HttpUtility.HtmlEncode(enumName));
                }
            }
            else
            {
                text.AppendFormat("<option value='0'>Select {0}</option>",
                    item.PropertyName.PascalCaseToText());

                foreach (var enumName in enumNames)
                {
                    if (item.Value.ToString() == enumName)
                    {
                        text.AppendFormat("<option selected value='{0}'>{1}</option>",
                        enumName, HttpUtility.HtmlEncode(enumName));
                    }
                    else
                    {
                        text.AppendFormat("<option value='{0}'>{1}</option>",
                      enumName, HttpUtility.HtmlEncode(enumName));
                    }
                }
            }

            text.Append("</select>");
            container.Text = text.ToString();
        }
    }

    public class ObjectProperty
    {
        public ObjectProperty(
            string propertyName, Type propertyType,
            object value, bool canBeModified)
        {
            this.PropertyName = propertyName;
            this.PropertyType = propertyType;
            this.Value = value;
            this.CanBeModified = canBeModified;
        }

        public string PropertyName { get; set; }

        public string LabelName
        {
            get
            {
                return this.PropertyName.PascalCaseToText();

            }
        }

        public Type PropertyType { get; set; }

        public object Value { get; set; }

        public bool CanBeModified { get; set; }

        public object Selected { get; set; }

        public string SelectDefaultValue { get; set; }
    }
}