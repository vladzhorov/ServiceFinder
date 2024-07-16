using Microsoft.AspNetCore.Http;

namespace ServiceFinder.OrderService.Domain.Exceptions
{
    public class ModelNotFoundException : Exception
    {
        public int StatusCode { get; } = StatusCodes.Status404NotFound;
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