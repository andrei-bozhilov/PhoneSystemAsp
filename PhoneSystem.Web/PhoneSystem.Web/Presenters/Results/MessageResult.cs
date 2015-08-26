namespace PhoneSystem.Web.Presenters.Results
{
    using System.Collections.Generic;

    public class MessageResult : IResult
    {
        public IList<string> Messages { get; set; }

        public string RedirectUrl { get; set; }

        public MessageResult()
        {
            this.Messages = new List<string>();
        }

        public MessageResult(IList<string> messages)
        {
            this.Messages = messages;
        }

        public MessageResult(string messages)
            : this(messages, null)
        {
        }

        public MessageResult(string messages, string redirectUrl)
        {
            this.Messages = new List<string> { messages };
            this.RedirectUrl = redirectUrl;
        }

        public void Add(string messages)
        {
            this.Messages.Add(messages);
        }
    }
}