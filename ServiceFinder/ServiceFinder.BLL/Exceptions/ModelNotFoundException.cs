namespace ServiceFinder.BLL.Exceptions
{
    public class ModelNotFoundException : Exception
    {
        public int StatusCode { get; } = 404;
        public Guid ModelId { get; }

        public ModelNotFoundException() { }

        public ModelNotFoundException(string message, Exception innerException) : base(message, innerException) { }

        public ModelNotFoundException(string message) : base(message) { }

        public ModelNotFoundException(Guid modelId, string message) : base(message)
        {
            ModelId = modelId;
        }
        public ModelNotFoundException(Guid modelId) : base($"Model with Id={modelId} doesn't exist.")
        {
            ModelId = modelId;
        }
    }
}