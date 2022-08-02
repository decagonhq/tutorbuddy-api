using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBuddy.Core.Utilities
{
    public static class TokenConverter
    {
        public static string EncodeToken(string token)
        {
            return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        }

        public static string DecodeToken(string token)
        {
            return Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
        }
    }
}
