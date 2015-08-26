namespace PhoneSystem.Web.Presenters.Results
{
    using System.Collections.Generic;

    public class ErrorResult : IResult
    {
        public IList<string> Errors { get; set; }

        public string RedirectUrl { get; set; }

        public int Count
        {
            get
            {
                return this.Errors.Count;
            }
        }

        public ErrorResult()
        {
            this.Errors = new List<string>();
        }

        public ErrorResult(IList<string> error)
        {
            this.Errors = error;
        }

        public ErrorResult(string error)
            : this(error, null)
        {
        }

        public ErrorResult(string error, string redirectUrl)
        {
            this.Errors = new List<string> { error };
            this.RedirectUrl = redirectUrl;
        }

        public void Add(string error)
        {
            this.Errors.Add(error);
        }
    }
}