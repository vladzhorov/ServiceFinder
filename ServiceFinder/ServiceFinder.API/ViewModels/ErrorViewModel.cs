using System.Text.Json;

namespace ServiceFinder.API.ViewModels
{
    public class ErrorViewModel
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
