namespace PhoneSystem.Web.ViewModels.Admin.TakeOrder
{
    using System.Collections.Generic;

    public class TakeOrderInfoViewModel
    {
        public UserViewModel UserInfo { get; set; }

        public IEnumerable<PhoneInfoViewModel> PhonesInfo { get; set; }

        public IEnumerable<OrdersViewModel> OrdersInfo { get; set; }
    }
}