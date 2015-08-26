namespace PhoneSystem.Web.Presenters.Admin
{
    using System;
    using System.Linq;

    using AutoMapper.QueryableExtensions;

    using PhoneSystem.Web.ViewModels.Admin;
    using System.Collections.Generic;
    using PhoneSystem.Web.ViewModels.Admin.Phonebook;

    public class PhonebookPresenter : BasePresenter<IQueryable<PhonebookViewModel>>
    {
        protected static readonly string AllNoLetterAndNumberChars = "Others";

        public override IResult GetResult()
        {
            var model = this.GetResultQueryable();

            return this.DataResult(model);
        }

        public override IResult GetById<TModel>(object id)
        {
            throw new NotImplementedException();
        }

        public IResult GetGroupByParameterData(string groupByParam, string getParam)
        {
            var dataGroupBy = new Dictionary<string, List<PhonebookViewModel>>();

            switch (groupByParam.ToLower())
            {
                case "name":
                    dataGroupBy = this.GetGroupByUserName(getParam);
                    break;
                case "department":
                    dataGroupBy = this.GetGroupByDepartment(getParam);
                    break;
                case "jobtitle":
                    dataGroupBy = this.GetGroupByJobTitle(getParam);
                    break;
                case "phone":
                    dataGroupBy = this.GetGroupByPhone(getParam);
                    break;
                default:
                    break;
            }

            return this.DataResult(dataGroupBy);
        }

        public IResult GetLetters(string groupByParam)
        {
            List<string> dataLetters = new List<string>();

            switch (groupByParam.ToLower())
            {
                case "name":
                    dataLetters = this.GetLettersByUserName();
                    break;
                case "department":
                    dataLetters = this.GetLettersByDepartment();
                    break;
                case "jobtitle":
                    dataLetters = this.GetLettersByJobTitle();
                    break;
                case "phone":
                    break;
                default:
                    break;
            }

            return this.DataResult(dataLetters);
        }

        private IQueryable<PhonebookViewModel> GetResultQueryable()
        {
            var model = this.Data.Phones
                .GetNotFreePhones()
                .Project().To<PhonebookViewModel>();

            return model;
        }

        private Dictionary<string, List<PhonebookViewModel>> GetGroupByPhone(string getParam)
        {
            var result = this.GetResultQueryable()
                    .Where(x => x.PhoneNumber.Contains(getParam))
                    .GroupBy(k => k.PhoneNumber)
                    .OrderBy(k => k.Key)
                    .ToDictionary(k => k.Key, k => k.OrderBy(x => x.UserName).ToList());

            return result;
        }

        private Dictionary<string, List<PhonebookViewModel>> GetGroupByDepartment(string getParam)
        {
            if (getParam == AllNoLetterAndNumberChars)
            {
                return this.GetResultQueryable()
                    .GroupBy(k => k.DepartmentName)
                    .OrderBy(k => k.Key)
                    .ToList()
                    .Where(x => !char.IsLetterOrDigit((x.Key.FirstOrDefault())))
                    .ToDictionary(k => k.Key, k => k.OrderBy(x => x.UserName).ToList());
            }
            else
            {
                return this.GetResultQueryable()
                    .Where(x => x.DepartmentName.StartsWith(getParam))
                    .GroupBy(k => k.DepartmentName)
                    .OrderBy(k => k.Key)
                    .ToDictionary(k => k.Key, k => k.OrderBy(x => x.UserName).ToList());
            }
        }

        private Dictionary<string, List<PhonebookViewModel>> GetGroupByJobTitle(string getParam)
        {
            if (getParam == AllNoLetterAndNumberChars)
            {
                return this.GetResultQueryable()
                   .GroupBy(k => k.JobTitle)
                   .OrderBy(k => k.Key)
                   .ToList()
                   .Where(x => !char.IsLetterOrDigit((x.Key.FirstOrDefault())))
                   .ToDictionary(k => k.Key, k => k.OrderBy(x => x.UserName).ToList());
            }
            else
            {
                return this.GetResultQueryable()
                   .Where(x => x.JobTitle.StartsWith(getParam))
                   .GroupBy(k => k.JobTitle)
                   .OrderBy(k => k.Key)
                   .ToDictionary(k => k.Key, k => k.OrderBy(x => x.UserName).ToList());
            }
        }

        private Dictionary<string, List<PhonebookViewModel>> GetGroupByUserName(string getParam)
        {
            if (getParam == AllNoLetterAndNumberChars)
            {
                return this.GetResultQueryable()
                   .GroupBy(k => k.UserName)
                   .OrderBy(k => k.Key)
                   .ToList()
                   .Where(x => !char.IsLetterOrDigit((x.Key.FirstOrDefault())))
                   .ToDictionary(k => k.Key, k => k.OrderBy(x => x.UserName).ToList());
            }
            else
            {
                return this.GetResultQueryable()
                   .Where(x => x.UserName.StartsWith(getParam))
                   .GroupBy(k => k.UserName)
                   .OrderBy(k => k.Key)
                   .ToDictionary(k => k.Key, k => k.OrderBy(x => x.UserName).ToList());
            }
        }

        private List<string> GetLettersByJobTitle()
        {
            var result = this.GetResultQueryable()
                .Select(x => x.JobTitle.Substring(0, 1).ToLower())
                .OrderBy(x => x)
                .Distinct()
                .ToList();

            result = this.Sanitaze(result);

            return result;
        }

        private List<string> GetLettersByDepartment()
        {
            var result = this.GetResultQueryable()
                .Select(x => x.DepartmentName.Substring(0, 1).ToLower())
                .OrderBy(x => x)
                .Distinct()
                .ToList();

            result = this.Sanitaze(result);

            return result;

        }

        private List<string> GetLettersByUserName()
        {
            var result = this.GetResultQueryable()
               .Select(x => x.UserName.Substring(0, 1).ToLower())
               .OrderBy(x => x)
               .Distinct()
               .ToList();

            result = this.Sanitaze(result);

            return result;
        }

        private List<string> Sanitaze(List<string> collection)
        {
            var result = new List<string>();
            bool hasValueDifferentThanLetter = false;
            foreach (var str in collection)
            {
                char ch = Convert.ToChar(str);
                if (char.IsLetterOrDigit(ch))
                {
                    result.Add(ch.ToString());
                }
                else
                {
                    hasValueDifferentThanLetter = true;
                }
            }

            if (hasValueDifferentThanLetter)
            {
                result.Add(AllNoLetterAndNumberChars);
            }

            return result;
        }
    }
}