namespace PhoneSystem.Web.Presenters.Admin
{
    using System;
    using System.Linq;
    using System.Web.UI.WebControls;

    using Microsoft.AspNet.Identity;
    using AutoMapper.QueryableExtensions;

    using PhoneSystem.Models;
    using PhoneSystem.Web.ViewModels.Admin;
    using BindModels.Admin;
    using Results;
    using PhoneSystem.Web.ViewModels.Admin.GiveOrder;

    public class GiveOrderPresenter : BasePresenter<GiveOrderViewModel>
    {
        public override IResult GetResult()
        {
            GiveOrderViewModel model = new GiveOrderViewModel();
            IQueryable<ListItem> departments = this.Data.Departments.All()
                .OrderBy(x => x.Name)
                .Select(x =>
                new ListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });

            IQueryable<ListItem> jobTitles = this.Data.JobTitles.All()
                .OrderBy(j => j.Name)
                .Select(j =>
                new ListItem()
                {
                    Text = j.Name,
                    Value = j.Id.ToString()
                });

            IQueryable<ListItem> freePhoneNumbers = this.Data.Phones.GetFreePhones()
                .OrderBy(p => p.PhoneNumber)
                .Select(p =>
                new ListItem()
                {
                    Text = p.PhoneNumber,
                    Value = p.Id.ToString()
                });

            IQueryable<string> users = this.Data.Users.All()
                .OrderBy(u => u.UserName)
                .Select(u => u.UserName);


            model.Departments = departments;
            model.JobTitles = jobTitles;
            model.FreePhoneNumbers = freePhoneNumbers;
            model.Users = users;

            return this.DataResult(model);
        }

        public IResult CreateUser(GiveOrderBindingModel insertModel, ApplicationUserManager manager, ApplicationSignInManager signInManager)
        {
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            User user = new User()
            {
                UserName = insertModel.Username,
                FullName = insertModel.FullName,
                Email = insertModel.Username + "@temp.com",
                IsActive = true,
                DepartmentId = insertModel.DepartmentId,
                JobTitleId = insertModel.JobTitleId,
                CreatedOn = DateTime.Now
            };

            IdentityResult result = manager.Create(user, insertModel.Password);
            if (result.Succeeded)
            {
                return this.DataResult(user);
            }
            else
            {
                return this.ErrorResult(string.Join(", ", result.Errors));
            }
        }

        public IResult CreatePhoneOrder(string currentUserId, string userId, int phoneId)
        {
            Phone phone = this.Data.Phones.GetById(phoneId);

            PhoneNumberOrder order = new PhoneNumberOrder()
            {
                ActionDate = DateTime.Now,
                AdminId = currentUserId,
                PhoneAction = PhoneAction.TakePhone,
                UserId = userId,
                PhoneNumber = phone.PhoneNumber
            };

            phone.PhoneStatus = PhoneStatus.Taken;
            phone.UserId = userId;
            this.Data.Phones.Update(phone);
            this.Data.PhoneNumberOrders.Add(order);

            try
            {
                this.Data.SaveChanges();
            }
            catch (Exception)
            {
                return this.ErrorResult("There was a problem.");
            }

            return this.MessageResult("Order is created!", "/admin/orders");
        }

        public IResult GetUserData(string username)
        {
            UserViewModel userModel = this.GetUserDataModel(username);
            if (userModel == null)
            {
                userModel = new UserViewModel();
                userModel.DepartmentName = "There is no such user.";
                userModel.JobTitleName = "There is no such user.";
            }

            return this.DataResult(userModel);
        }

        public override IResult GetById<TModel>(object id)
        {
            throw new NotImplementedException();
        }

        public IResult CreatOrderFromOldUser(string username, string userId, int phoneId)
        {
            UserViewModel user = this.GetUserDataModel(username);

            if (user == null)
            {
                return this.ErrorResult("There is no such user.");
            }

            IResult result = this.CreatePhoneOrder(userId, user.Id, phoneId);
            return result;
        }

        private UserViewModel GetUserDataModel(string username)
        {
            if (username == null)
            {
                return null;
            }

            UserViewModel userModel = this.Data.Users
                   .Find(x => x.UserName == username)
                   .Project().To<UserViewModel>()
                   .FirstOrDefault();

            return userModel;
        }
    }
}