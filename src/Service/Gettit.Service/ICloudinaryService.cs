using Microsoft.AspNetCore.Http;

namespace Gettit.Service
{
    public interface ICloudinaryService
    {
        Task<Dictionary<string, object>> UploadFile(IFormFile file);
    }
}
