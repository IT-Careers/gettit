using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Gettit.Service.Cloud
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly IConfiguration configuration;

        private readonly ILogger<CloudinaryService> _logger;

        private const string CloudinaryUrl = "https://api.cloudinary.com/v1_1/{0}/auto/upload";

        public CloudinaryService(IConfiguration configuration, ILogger<CloudinaryService> logger)
        {
            this.configuration = configuration;
            _logger = logger;
        }

        private string GetUnixTimestamp()
        {
            return ((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString();
        }

        private string GetApiKey()
        {
            return configuration["Cloudinary:ApiKey"];
        }

        private string GetApiSecret()
        {
            return configuration["Cloudinary:ApiSecret"];
        }

        private string EncodeWithSha256(string value)
        {
            StringBuilder stringBuilder = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(value));

                foreach (byte b in result) stringBuilder.AppendFormat("{0:x2}", b);
            }

            return stringBuilder.ToString();
        }

        private string GetSignature(string timestamp, string publicId)
        {
            return EncodeWithSha256($"public_id={publicId}&timestamp={timestamp}{GetApiSecret()}");
        }

        private byte[] ReadFileBytes(IFormFile formFile)
        {
            if (formFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    formFile.CopyTo(ms);
                    return ms.ToArray();
                }
            }

            return new byte[0];
        }

        public async Task<Dictionary<string, object>> UploadFile(IFormFile formFile)
        {
            var currentTimestamp = GetUnixTimestamp();
            var apiKey = GetApiKey();
            var publicId = Guid.NewGuid().ToString() + ":" + StripExtension(formFile.FileName);
            var signature = GetSignature(currentTimestamp, publicId);

            string file = Convert.ToBase64String(ReadFileBytes(formFile));
            var json = JsonConvert.SerializeObject(new
            {
                file = HttpUtility.HtmlEncode($"data:{formFile.ContentType};base64,") + file,
                timestamp = currentTimestamp,
                public_id = publicId,
                api_key = apiKey,
                signature
            });

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, string.Format(CloudinaryUrl, configuration["Cloudinary:CloudName"]))
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            var httpClient = new HttpClient();
            var httpResponse = await httpClient.SendAsync(httpRequest);

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseJson = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Dictionary<string, object>>(responseJson);
            }

            var deserializedResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(await httpResponse.Content.ReadAsStringAsync());
            _logger.LogError(deserializedResponse["error"].ToString());

            return null;
        }

        private string StripExtension(string fileName)
        {
            return fileName
                .Replace(".png", "")
                .Replace(".jpg", "")
                .Replace(".jpeg", "")
                .Replace(".gif", "")
                .Replace(".mp4", "")
                .Replace(".mkv", "");
        }
    }
}
