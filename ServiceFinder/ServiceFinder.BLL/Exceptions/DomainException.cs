namespace ServiceFinder.BLL.Exceptions
{
    public class DomainException : Exception
    {
        public int StatusCode { get; } = 400;

        public DomainException() { }

        public DomainException(string message, Exception innerException) : base(message, innerException) { }

        public DomainException(string message) : base(message) { }
    }
}

