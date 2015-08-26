namespace PhoneSystem.Web.Presenters.Admin
{
    using System;
    using System.Linq;

    using AutoMapper.QueryableExtensions;
    using PhoneSystem.Models;
    using PhoneSystem.Web.ViewModels.Admin.TakeOrder;
    using Results;

    public class TakeOrderPresenter : BasePresenter<TakeOrderViewModel>, IPresenter<TakeOrderViewModel>
    {
        public override IResult GetResult()
        {
            var model = new TakeOrderViewModel();
            model.PhoneNumbers = this.Data.Phones.All()
                .Where(x => x.PhoneStatus == PhoneStatus.Taken)
                .Select(x => x.PhoneNumber);

            model.UserNames = this.Data.Users.All()
                .Select(x => x.UserName);

            return this.DataResult(model);
        }

        public IResult GetInfo(string phoneId, string username)
        {
            TakeOrderInfoViewModel model = new TakeOrderInfoViewModel();

            if (string.IsNullOrWhiteSpace(phoneId) && string.IsNullOrWhiteSpace(username))
            {
                return this.ErrorResult("Error: Both phone and username are empty.");
            }
            else if (!string.IsNullOrWhiteSpace(phoneId) && !string.IsNullOrWhiteSpace(username))
            {
                return this.ErrorResult("Error: Both phone and username have data. Please use one of them.");
            }
            else if (!string.IsNullOrWhiteSpace(phoneId))
            {
                var order = this.Data.PhoneNumberOrders.All()
                    .Where(o => o.PhoneNumber == phoneId)
                    .OrderByDescending(o => o.ActionDate).FirstOrDefault();

                if (order == null)
                {
                    return this.ErrorResult("Message: There isn't order with this phone. It is never use.");
                }

                model = this.GetInfoForUser(order.UserId);
            }
            else if (!string.IsNullOrWhiteSpace(username))
            {
                var userId = this.Data.Users.All()
                    .Where(x => x.UserName == username)
                    .Select(x => x.Id)
                    .FirstOrDefault();

                if (userId == null)
                {
                    return this.ErrorResult("Message: There is no such user.");
                }

                model = this.GetInfoForUser(userId);
            }

            return this.DataResult(model);
        }

        public IResult TakePhone(string phoneId, string currentUserId)
        {
            TakeOrderResultViewModel model = new TakeOrderResultViewModel();

            Phone phone = this.Data.Phones.All()
                .Where(p => p.PhoneNumber == phoneId)
                .FirstOrDefault();

            if (phone == null)
            {
                return this.ErrorResult("Error: There is no such phone");
            }

            if (phone.PhoneStatus != PhoneStatus.Taken)
            {
                return this.ErrorResult(string.Format("Error: Phone status is {0}. It can be taken back.", phone.PhoneStatus.ToString()));
            }

            PhoneNumberOrder takeOrder = this.Data.PhoneNumberOrders.All()
                .Where(p => p.PhoneNumber == phoneId)
                .OrderByDescending(p => p.Id)
                .FirstOrDefault();

            if (takeOrder == null)
            {
                return this.ErrorResult(string.Format("Error: There is no order for this phone {0}", phoneId));
            }

            if (takeOrder.PhoneAction != PhoneAction.TakePhone)
            {
                return this.ErrorResult(string.Format("Error: Order status is {0} for phone {1}. It can be taken back.",
                    takeOrder.PhoneAction.ToString(), phoneId));
            }

            PhoneNumberOrder order = new PhoneNumberOrder()
            {
                ActionDate = DateTime.Now,
                AdminId = currentUserId,
                PhoneNumber = phoneId,
                UserId = takeOrder.UserId,
                PhoneAction = PhoneAction.GiveBackPhone
            };

            User user = this.Data.Users.GetById(order.UserId);
            user.IsActive = true;

            this.Data.PhoneNumberOrders.Add(order);
            this.Data.Users.Update(user);
            phone.PhoneStatus = PhoneStatus.Free;
            phone.UserId = null;
            this.Data.Phones.Update(phone);

            // TODO phone available after logic

            try
            {
                this.Data.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorResult errors = new ErrorResult();
                foreach (var key in ex.Data.Keys)
                {
                    errors.Add(key + " " + ex.Data[key]);
                }

                return errors;
            }

            MessageResult message = new MessageResult();
            message.Add(string.Format("Order with id {0} was created", order.Id));
            message.Add(string.Format("Phone with id {0} was set to free and available after {1}", phone.PhoneNumber, phone.AvailableAfter));
            return message;
        }

        private TakeOrderInfoViewModel GetInfoForUser(string userId)
        {
            var model = new TakeOrderInfoViewModel();

            model.UserInfo = this.Data.Users.All()
                .Where(x => x.Id == userId)
                .Project().To<UserViewModel>()
                .FirstOrDefault();

            model.OrdersInfo = this.Data.PhoneNumberOrders.All()
                .Where(o => o.UserId == userId)
                .Project().To<OrdersViewModel>()
                .OrderByDescending(o => o.ActionDate)
                .ToList();

            model.PhonesInfo = this.Data.Phones.All()
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.ModifiedOn)
                .Project().To<PhoneInfoViewModel>()
                .ToList();

            return model;
        }

        public override IResult GetById<TModel>(object id)
        {
            throw new NotImplementedException();
        }
    }
}