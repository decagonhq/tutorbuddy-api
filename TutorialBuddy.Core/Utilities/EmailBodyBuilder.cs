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
            //var link = string.Empty;
            TextInfo textInfo = new CultureInfo("en-GB", false).TextInfo;
            var userName = textInfo.ToTitleCase(user.FirstName ?? "");

            //foreach (var role in userRole)
            //{
            //    if (role == UserRole.Tutor.ToString() || role == UserRole.Student.ToString())
            //    {
            //        link = $"https://tutorbuddyapi.herokuapp.com/{controllerName}/{linkName}?email={user.Email}&token={token}";
            //    }
            //    else
            //    {
            //        link = $"http://www.example.com/{linkName}/{token}/{user.Email}";
            //    }
            //}
            Log.Information("About to get the static email file");
            var temp = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), emailTempPath));
            Log.Information($"Successfull get email path: {temp}");
            var newTemp = temp.Replace("**code**", token);
            var emailBody = newTemp.Replace("**user**", userName);
            return emailBody;
        }

        
    }
}
