namespace PhoneSystem.Web.Controls.Attibutes
{
    using System;

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    sealed class NotMapPropertyAttribute : Attribute
    {
        public NotMapPropertyAttribute()
        {
        }
    }
}