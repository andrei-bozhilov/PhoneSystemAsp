namespace PhoneSystem.Web.Presenters.Admin
{
    using System.Linq;

    using AutoMapper.QueryableExtensions;

    using PhoneSystem.Web.ViewModels.Admin;
    using PhoneSystem.Common;
    using PhoneSystem.Web.ViewModels.Admin.GiveOrder;

    public class UserPresenter : BasePresenter<IQueryable<UserViewModel>>
    {
        public override IResult GetResult()
        {
            string adminId = this.Data.UserRoles.All()
                .Where(x => x.Name == GlobalConstants.AdminRole)
                .Select(x => x.Id)
                .FirstOrDefault();

            var users = this.Data.Users.All()
                .OrderByDescending(x => x.CreatedOn)
                .Project().To<UserViewModel>(new { adminId = adminId });

            return this.DataResult(users);
        }

        public override IResult GetById<TModel>(object id)
        {
            return this.RedirectResult("/Admin/UserDetails/" + id);
        }
    }
}