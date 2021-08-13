using System;
using System.Collections.Generic;

namespace Core.Models.Responses
{
    public class UserManagerResponse
    {
        public string Message { get; set; }

        public bool IsSuccess { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public DateTimeOffset? ExpirationDate { get; set; }
    }
}