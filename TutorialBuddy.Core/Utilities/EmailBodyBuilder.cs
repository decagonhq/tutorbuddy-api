using System.Globalization;
using Serilog;
using TutorBuddy.Core.Enums;
using TutorBuddy.Core.Models;

namespace TutorBuddy.Core.Utilities
{
    public class EmailBodyBuilder
    {
        public static async Task<string> GetEmailBody(User user, List<string> userRole, string emailTempPath, string linkName, string token, string controllerName)
        {
            var link = string.Empty;
            TextInfo textInfo = new CultureInfo("en-GB", false).TextInfo;
            var userName = textInfo.ToTitleCase(user.FirstName ?? "");

            foreach (var role in userRole)
            {
                if (role == UserRole.Tutor.ToString() || role == UserRole.Student.ToString())
                {
                    link = $"https://tutorbuddyapi.herokuapp.com/{controllerName}/{linkName}?email={user.Email}&token={token}";
                }
                else
                {
                    link = $"http://www.example.com/{linkName}/{token}/{user.Email}";
                }
            }
            Log.Information("About to get the static email file");
            var temp = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), emailTempPath));
            Log.Information($"Successfull get email path: {temp}");
            var newTemp = temp.Replace("**link**", link);
            var emailBody = newTemp.Replace("**user**", userName);
            return emailBody;
        }

        //public static async Task<string> GetEmailBody(string emailTempPath, string token, string email)
        //{
        //    var link = $"https://tutorbuddyapi.herokuapp.com/Manager/RegisterManager?email={email}&token={token}";
        //    var temp = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), emailTempPath));
        //    var emailBody = temp.Replace("**link**", link);
        //    return emailBody;
        //}
    }
}
