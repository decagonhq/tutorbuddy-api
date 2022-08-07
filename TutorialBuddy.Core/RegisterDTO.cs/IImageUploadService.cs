using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using TutorBuddy.Core.Models;
using TutorialBuddy.Infastructure.Services;

namespace TutorialBuddy.Core
{
    public interface IImageUploadService
    {
        Task<UploadResult> UploadSingleImage(IFormFile file, string tag);
        Task<ApiResponse<bool>> DeleteByPublicId(string publicId);
        Task<ApiResponse<bool>> DeleteByTag(string tag);
        Task<List<ImageMeta>> UploadImages(MultiImageUploadDto upload, string tag);
    }
}