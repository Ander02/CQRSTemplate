﻿using CQRSTemplate.Infraestructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace CQRSTemplate.Util
{
    public static class Extensions
    {
        public static string ToBase64(this byte[] buffer) => Convert.ToBase64String(buffer);

        public static byte[] FromBase64(this string base64) => Convert.FromBase64String(base64);

        public static string RemoveAccentuation(this string s)
        {
            string normalizedString = s.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }

        public static bool NotEquals(this object obj1, object obj2) => !obj1.Equals(obj2);

        public static async Task MakeErrorResponse(this HttpContext context, int statusCode, dynamic errorMessage)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            byte[] buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new ExceptionResult()
            {
                Error = errorMessage
            }));
            await context.Response.Body.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}
