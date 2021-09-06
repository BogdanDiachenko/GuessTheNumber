using System;
using System.Collections.Generic;
using Core.Models.DTOs;
using Core.Models.Identity;

namespace Core.Models.Responses
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }

        public T Value { get; set; }

        public string Error { get; set; }

        public static Response<T> Success(T value) => new Response<T> { IsSuccess = true, Value = value };

        public static Response<T> Failure(string error) => new Response<T> { IsSuccess = false, Error = error };
    }
}