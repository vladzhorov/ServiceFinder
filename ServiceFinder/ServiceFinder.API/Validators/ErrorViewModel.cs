﻿using System.Text.Json;

namespace ServiceFinder.API.Validators
{
    public class ErrorViewModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}