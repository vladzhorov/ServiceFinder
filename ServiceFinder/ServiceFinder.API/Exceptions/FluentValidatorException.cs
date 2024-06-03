namespace ServiceFinder.API.Exceptions
{

    public class FluentValidatorException : Exception
    {
        public int StatusCode { get; } = 400;

        public FluentValidatorException() { }

        public FluentValidatorException(string message, Exception innerException) : base(message, innerException) { }

        public FluentValidatorException(string message) : base(message) { }
    }
}
