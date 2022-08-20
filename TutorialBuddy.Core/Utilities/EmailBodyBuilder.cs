using System.Globalization;
using Serilog;
using TutorBuddy.Core.Enums;
using TutorBuddy.Core.Models;

namespace TutorBuddy.Core.Utilities
{
    public class EmailBodyBuilder
    {
        public static async Task<string> GetEmailBody(User user, string emailTempPath, string linkName, string token)
        {
            
            TextInfo textInfo = new CultureInfo("en-GB", false).TextInfo;
            var userName = textInfo.ToTitleCase(user.FirstName ?? "");

            
            Log.Information("About to get the static email file");
            var temp = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), emailTempPath));
            Log.Information($"Successfull get email path: {temp}");
            var newTemp = temp.Replace("**code**", token);
            var emailBody = newTemp.Replace("**user**", userName);
            return emailBody;
        }

        
    }
}
