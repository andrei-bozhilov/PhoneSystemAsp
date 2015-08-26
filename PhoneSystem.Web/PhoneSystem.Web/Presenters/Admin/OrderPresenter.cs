namespace PhoneSystem.Web.Presenters.Admin
{
    using System.Linq;

    using AutoMapper.QueryableExtensions;

    using PhoneSystem.Web.ViewModels.Admin;
    using System;
    using PhoneSystem.Web.ViewModels.Admin.Orders;

    public class OrderPresenter : BasePresenter<IQueryable<OrdersViewModel>>
    {
        public override IResult GetResult()
        {
            var data = this.Data.PhoneNumberOrders.All()
                .OrderByDescending(o => o.ActionDate)
                .Project().To<OrdersViewModel>();               

            return this.DataResult(data);
        }

        public override IResult GetById<TModel>(object id)
        {
            throw new NotImplementedException();
        }
    }
}