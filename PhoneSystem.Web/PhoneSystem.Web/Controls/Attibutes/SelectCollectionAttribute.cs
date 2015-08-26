namespace PhoneSystem.Web.Controls.Attibutes
{
    using System;

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    sealed class SelectCollectionAttribute : Attribute
    {
        public SelectCollectionAttribute(string selectFor, string selectDefaultValue)
        {
            this.SelectFor = selectFor;
            this.SelectDefaultValue = selectDefaultValue;
        }

        public string SelectFor { get; set; }

        public string SelectDefaultValue { get; set; }
    }
}