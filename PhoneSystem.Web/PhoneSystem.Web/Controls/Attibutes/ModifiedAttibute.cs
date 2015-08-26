namespace PhoneSystem.Web.Controls.Attibutes
{
    using System;

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    sealed class CanBeModifiedAttribute : Attribute
    {
        public CanBeModifiedAttribute(bool canBeModified)
        {
            this.Modified = canBeModified;
        }

        public bool Modified { get; set; }
    }
}