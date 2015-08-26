namespace PhoneSystem.Web.ViewModels.Admin.TakeOrder
{
    using System;
    using System.Collections.Generic;

    public class TakeOrderResultViewModel
    {
        public TakeOrderResultViewModel()
        {
            this.Errors = new List<string>();
            this.Messages = new List<string>();
        }

        public IList<string> Errors { get; set; }

        public IList<string> Messages { get; set; }
    }
}