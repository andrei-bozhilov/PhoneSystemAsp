namespace PhoneSystem.Web.Presenters.Results
{
    public class DataResult<T> : IResult
    {
        private T dataItem;

        public DataResult(T data)
        {
            dataItem = data;
        }

        public T DataItem
        {
            get
            {
                return dataItem;
            }
        }
    }
}