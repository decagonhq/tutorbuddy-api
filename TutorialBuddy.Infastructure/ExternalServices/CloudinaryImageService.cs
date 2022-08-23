using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TutorialBuddy.Core;
using TutorBuddy.Core.Models;
using TutorBuddy.Infrastructure.DataAccess;
using Serilog;

namespace TutorialBuddy.Infastructure.Services
{
    public class ImageUploadService : IImageUploadService
    {
        private readonly TutorBuddyContext _context;
        private readonly IConfiguration _configuration;
        private readonly Cloudinary _cloudinary;
        private CloudinarySettings _cloudinaryOptions;

        public ImageUploadService(IServiceProvider provider, IConfiguration configuration)
        {
            _context = provider.GetRequiredService<TutorBuddyContext>();
            _configuration = configuration;
            _cloudinary = new Cloudinary(new Account(_configuration.GetValue<string>("CloudinarySettings:CloudName"),
               _configuration.GetValue<string>("CloudinarySettings:ApiKey"), _configuration.GetValue<string>("CloudinarySettings:ApiSecret")));
        }

        public int ValidateImage(IFormFile image)
        {
            // validate the image size and extension type using settings from appsettings
            var status = -1;
            var listOfextensions = _configuration.GetSection("PhotoSettings:Extensions").Get<List<string>>();
            for (int i = 0; i < listOfextensions.Count; i++)
            {
                if (image.FileName.EndsWith(listOfextensions[i]))
                {
                    status = 0;
                    break;
                }
            }
            var pixSize = Convert.ToInt64(_configuration.GetSection("PhotoSettings:Size").Get<string>());
            if (image == null || image.Length > pixSize)
                return _ = -1;
            if (status != 0)
                status = -2;
            return status;
        }

        /// <summary>
        /// Upload a collection of images.
        /// </summary>
        /// <param name="upload">Collection Of Images</param>
        /// <param name="tag">unique tag to associate with owner</param>
        /// <returns></returns>
        public async Task<List<ImageMeta>> UploadImages(MultiImageUploadDto upload, string tag)
        {
            List<ImageMeta> imageMetadatas = new List<ImageMeta>();

            if (upload.Images == null) return imageMetadatas;

            foreach (var image in upload.Images)
            {
                if (ValidateImage(image) != 0) return imageMetadatas;
            }

            foreach (var image in upload.Images)
            {
                var result = await UploadSingleImage(image, tag);
                var metadata = new ImageMeta { PublicId = result.PublicId, Url = result.Url.AbsoluteUri, Tag = tag };
                imageMetadatas.Add(metadata);
            }
            return imageMetadatas;
        }

        /// <summary>
        /// Uploads a single image
        /// </summary>
        /// <param name="file">Image data</param>
        /// <param name="tag">unique tag to associate with owner</param>
        /// <returns></returns>
        public async Task<UploadResult> UploadSingleImage(IFormFile file, string tag)
        {
            Log.Information("Decagon Enter the upload image service");
            //Runtime Complexity Check needed.
            var result = ValidateImage(file);
            var uploadResult = new ImageUploadResult();
            if (result != 0)
            {
                return default;
            }
            var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            await using var imageStream = file.OpenReadStream();
            var parameters = new ImageUploadParams()
            {
                File = new FileDescription(fileName, imageStream),
                PublicId = fileName,
                Tags = tag
            };
            Log.Information("Decagon Finish Image upload Setup, about load to cloundinary ");
            uploadResult = await _cloudinary.UploadAsync(parameters);
            Log.Information("Decagon Upload done to cloudinary successfully");
            return uploadResult;
        }

        public async Task<ApiResponse<bool>> DeleteByPublicId(string publicId)
        {
            var response = new ApiResponse<bool>();
            var imageMetadata = _context.ImageMeta.FirstOrDefault(Im => Im.PublicId == publicId);

            if (imageMetadata != null)
            {
                _context.ImageMeta.Remove(imageMetadata);
                _ = await _context.SaveChangesAsync();
                await _cloudinary.DeleteResourcesByPrefixAsync(publicId);
            }

            response.Message = "Image Deleted";
            response.Data = true;
            response.Success = true;
            return response;
        }

        public async Task<ApiResponse<bool>> DeleteByTag(string tag)
        {
            var response = new ApiResponse<bool>();
            var imageMeta = _context.ImageMeta.Where(Im => Im.PublicId == tag).ToList();

            if (imageMeta.Any())
            {
                _context.ImageMeta.RemoveRange(imageMeta);
                _ = await _context.SaveChangesAsync();
                await Task.Run(async () => await _cloudinary.DeleteResourcesByTagAsync(tag));
            }
            response.Message = "Images Deleted";
            response.Data = true;
            response.Success = true;
            return response;
        }
    }
}