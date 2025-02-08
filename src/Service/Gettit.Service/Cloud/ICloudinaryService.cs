using Microsoft.AspNetCore.Http;

namespace Gettit.Service.Cloud
{
    public interface ICloudinaryService
    {
        Task<Dictionary<string, object>> UploadFile(IFormFile file);
    }
}
