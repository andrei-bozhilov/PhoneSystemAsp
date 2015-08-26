namespace PhoneSystem.Web.Presenters.Admin
{
    using System;
    using System.Linq;
    using System.Web;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using AutoMapper.QueryableExtensions;
    using AutoMapper;

    using PhoneSystem.Models;
    using PhoneSystem.Web.ViewModels.Admin;
    using PhoneSystem.Web.ViewModels;
    using PhoneSystem.Common;
    using PhoneSystem.Web.ViewModels.Admin.Orders;
    using PhoneSystem.Web.ViewModels.Admin.UserDetails;

    public class UserDetailsPresenter : BaseCrudPresenter<UserDetailInfoViewModel, UserInfoViewModel>
    {
        public string UserId { get; set; }

        public UserDetailsPresenter(string userId)
        {
            this.UserId = userId;
        }

        public override IResult GetResult()
        {
            var user = this.Data.Users.All()
                .Where(x => x.Id == this.UserId)
                .Select(x => x.Id)
                .FirstOrDefault();

            if (user == null)
            {
                return this.ErrorResult("There is no such user", "/Admin/Users");
            }

            return this.GetById<object>(this.UserId);
        }

        public override IResult GetById<TModel>(object id)
        {
            var userInfo = new UserDetailInfoViewModel();
            var adminRoleId = this.Data.UserRoles.All()
                .Where(x => x.Name == GlobalConstants.AdminRole)
                .Select(x => x.Id)
                .FirstOrDefault();

            userInfo.UserInfo = this.Data.Users.All()
                .Where(x => x.Id == (string)id)
                .Project().To<UserInfoViewModel>(new { adminRoleId = adminRoleId })
                .FirstOrDefault();

            userInfo.UserInfo.Departments = this.Data.Departments.All()
                .OrderBy(d => d.Name)
                .Select(d => new KeyValuePair() { Key = d.Id.ToString(), Value = d.Name })
                .ToList();

            userInfo.UserInfo.JobTitles = this.Data.JobTitles.All()
                .OrderBy(d => d.Name)
                .Select(d => new KeyValuePair() { Key = d.Id.ToString(), Value = d.Name })
                .ToList();

            userInfo.OrdersInfo = this.Data.PhoneNumberOrders.All()
                .Where(o => o.UserId == (string)id)
                .OrderByDescending(o => o.ActionDate)
                .Project().To<OrdersViewModel>();

            userInfo.PhonesInfo = this.Data.Phones.All()
                .Where(p => p.UserId == (string)id)
                .OrderByDescending(p => p.ModifiedOn)
                .Project().To<PhoneInfoViewModel>();

            return this.DataResult(userInfo);
        }

        public override IResult Update(UserInfoViewModel entity)
        {
            var user = this.Data.Users.GetById(entity.Id);
            Mapper.CreateMap<User, UserInfoViewModel>();
            Mapper.Map(entity, user);
            this.Data.Users.Update(user);
            return this.SavaChanges("Successfully update user.");
        }

        public override IResult Delete(object id)
        {
            this.Data.Users.Delete(id);
            return this.SavaChanges("Successfully delete user.");
        }

        public override IResult UnDelete(object id)
        {
            this.Data.Users.UnDelete(id);
            return this.SavaChanges("Successfully undelete user.");
        }

        public override IResult Add(UserInfoViewModel entity)
        {
            throw new NotImplementedException();
        }

        public IResult ModifyUserToRoleAdmin(string userId, bool isCheched, HttpContext httpContext)
        {
            var manager = httpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            IdentityResult result;
            if (isCheched)
            {
                result = manager.RemoveFromRole(userId, GlobalConstants.AdminRole);
            }
            else
            {
                result = manager.AddToRole(userId, GlobalConstants.AdminRole);

            }

            if (result.Succeeded)
            {
                if (isCheched)
                {
                    return this.MessageResult("User is remove from role " + GlobalConstants.AdminRole);
                }
                else
                {
                    return this.MessageResult("User is add to role " + GlobalConstants.AdminRole);
                }

            }
            else
            {
                return this.ErrorResult(string.Join(", ", result.Errors));
            }
        }
    }
}