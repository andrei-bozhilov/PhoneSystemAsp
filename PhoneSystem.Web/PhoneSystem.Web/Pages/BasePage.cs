namespace PhoneSystem.Web.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI;

    using PhoneSystem.Common.Extensions;
    using PhoneSystem.Web.Presenters;
    using PhoneSystem.Web.Presenters.Results;
    using System.Web.UI.WebControls;
    using System;
    using Helpers;
    using System.Reflection;
    using System.ComponentModel.DataAnnotations;

    public enum MessageType
    {
        Success,
        Error
    }

    public abstract class BasePage<VM, P> : Page, IBasePage<VM, P>
         where P : IPresenter<VM>
    {
        private string pageName;

        public string PageName
        {
            get
            {
                var splitName = this.GetType().Name.Split('_');
                return this.pageName ?? splitName[splitName.Count() - 2].PascalCaseToText();
            }
            set
            {
                this.pageName = value;
            }
        }

        public P Presenter { get; set; }

        public VM ViewModel { get; set; }

        protected void AddMessage(MessageType mType, IEnumerable<string> message)
        {
            if (mType == MessageType.Success)
            {
                this.Session.Add("noty-success", message);
            }
            else if (mType == MessageType.Error)
            {
                this.Session.Add("noty-error", message);
            }
        }

        protected void ShowErrorsInModelState()
        {
            var errors = this.ModelState.Values
                .Select(x => string.Join(", ", x.Errors.Select(y => y.ErrorMessage)));
            this.AddMessage(MessageType.Error, errors);
        }

        protected void DisplayResult(int result, string successMessage, string errorMessage)
        {
            if (result > 0)
            {
                this.AddMessage(MessageType.Success, new string[] { successMessage });
            }
            else
            {
                this.AddMessage(MessageType.Error, new string[] { errorMessage });
            }

            this.Redirect(this.Request.Url.AbsoluteUri);
        }

        protected void Redirect(string url)
        {
            this.Response.Redirect(url);
        }

        public void TakeViewModel(P presenter)
        {
            this.Presenter = presenter;
            IResult result = this.Presenter.GetResult();
            this.TakeViewModel(result);
        }

        public void TakeViewModel(IResult result)
        {
            if (result is DataResult<VM>)
            {
                this.ViewModel = ((DataResult<VM>)result).DataItem;
            }
            else
            {
                this.TakeIResult(result);
            }
        }

        protected void TakeIResult<TResult>(IResult result, ref TResult model)
        {
            if (result is DataResult<TResult>)
            {
                model = ((DataResult<TResult>)result).DataItem;
            }
            else
            {
                this.TakeIResult(result);
            }
        }

        protected void TakeIResult(IResult result)
        {
            if (result is RedirectResult)
            {
                this.Redirect(((RedirectResult)result).Url);
            }
            else if (result is ErrorResult)
            {
                ErrorResult eResult = (ErrorResult)result;
                this.AddMessage(MessageType.Error, eResult.Errors);
                if (!string.IsNullOrWhiteSpace(eResult.RedirectUrl))
                {
                    this.Redirect(eResult.RedirectUrl);
                }
            }
            else if (result is MessageResult)
            {
                MessageResult mResult = (MessageResult)result;
                this.AddMessage(MessageType.Success, mResult.Messages);
                if (!string.IsNullOrWhiteSpace(mResult.RedirectUrl))
                {
                    this.Redirect(mResult.RedirectUrl);
                }
            }
        }

        protected string GetIdFromBtnSender(object sender)
        {
            Button btn = (Button)sender;
            return btn.CommandArgument;
        }

        public bool TryUpdateModel<T>(T model, string prefix)
        {
            var keys = this.Request.Form.AllKeys
                .Where(x => x.StartsWith(prefix));

            foreach (var key in keys)
            {
                string value = this.Request.Form[key];
                string keyWithoutPrefix = key.Replace(prefix, string.Empty);
                ReflectionHelper.SetValue(model, keyWithoutPrefix, value);
            }

            Type typeData = typeof(T);
            PropertyInfo[] props =
                typeData.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                var validationAttributes = prop.GetCustomAttributes<ValidationAttribute>();
                foreach (var validationAttribute in validationAttributes)
                {
                    object currentPropValue = prop.GetValue(model);
                    if (!validationAttribute.IsValid(currentPropValue))
                    {
                        string errorMessage = validationAttribute.FormatErrorMessage(prop.Name);
                        this.ModelState.AddModelError("Error", errorMessage);
                    }
                }
            }

            return this.ModelState.IsValid;
        }
    }
}