namespace PhoneSystem.Web.ViewModels.Admin.TakeOrder
{
    using System.Linq;

    public class TakeOrderViewModel
    {
        public IQueryable<string> UserNames { get; set; }

        public IQueryable<string> PhoneNumbers { get; set; }
    }
}