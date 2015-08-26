namespace PhoneSystem.Web.Controls.Attibutes
{
    using System;

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    sealed class EnumCollectionAttribute : Attribute
    {
        public Type EnumName { get; set; }

        public EnumCollectionAttribute(Type enumType)
        {
            this.EnumName = enumType;
        }
    }
}