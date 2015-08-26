namespace PhoneSystem.Web.Presenters.Admin
{
    using PhoneSystem.Web.Presenters.Results;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class HomePresenter : BasePresenter<IQueryable<String>>
    {
        public override IResult GetResult()
        {
            var data = this.Data.Users.All().Select(x => x.UserName);
            return this.DataResult(data);
        }

        public override IResult GetById<TModel>(object id)
        {
            throw new NotImplementedException();
        }
    }
}