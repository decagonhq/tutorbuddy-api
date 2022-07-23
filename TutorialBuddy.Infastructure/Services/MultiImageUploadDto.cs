using Microsoft.AspNetCore.Http;
using TutorialBuddy.Core.Models;

namespace TutorialBuddy.Infastructure.Services
{
    public class MultiImageUploadDto
    {
        public List<IFormFile> Images { get; set; }
    }
}