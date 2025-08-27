using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

namespace Service.Service.Implement;
public class CloudinaryService 
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IOptions<CloudinarySettings> config)
    {
        var settings = config.Value;
        var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
        _cloudinary = new Cloudinary(account);
    }

    public async Task<string> UploadImageAsync(IFormFile file)
    {
        if (file.Length <= 0) return null;

        await using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
        };
        var result = await _cloudinary.UploadAsync(uploadParams);

        return result.SecureUrl.ToString();
    }


    public async Task<string[]> UploadImagesAsync(List<IFormFile> files)
    {
      var urls = new List<string>();

      foreach (var file in files)
      {
        if (file.Length <= 0) continue;

        await using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
          File = new FileDescription(file.FileName, stream),
        };
        var result = await _cloudinary.UploadAsync(uploadParams);

        if (result.SecureUrl != null)
        {
          urls.Add(result.SecureUrl.ToString());
        }
      }

      return urls.ToArray();
    }

    public async Task<string> DeleteImageAsync(string publicId)
    {
        var deletionParams = new DeletionParams(publicId);
        var result = await _cloudinary.DestroyAsync(deletionParams);
        return result.Result;
    }

    public async Task<List<string>> GetAllImageUrlsAsync()
    {
        var result = await _cloudinary.ListResourcesAsync(new ListResourcesParams
        {
            ResourceType = ResourceType.Image,
            Type = "upload",
            MaxResults = 100
        });

        return result.Resources.Select(r => r.SecureUrl.ToString()).ToList();
    }
}
