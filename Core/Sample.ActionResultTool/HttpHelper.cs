using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sample.ActionResultTool
{
    public static class HttpHelper
    {
        private static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static StringContent JsonContent<T>(T value)
        {
            return new StringContent(JsonSerializer.Serialize(value, jsonSerializerOptions), Encoding.UTF8, "application/json");
        }

        // 扩展方法
        public static async Task<T?> ReadJsonContentAsync<T>(this HttpContent content) where T : class
        {
            T? result = null;
            try
            {
                result = await content.ReadFromJsonAsync<T>(jsonSerializerOptions);
            }
            catch { }
            return result;
        }
    }
}
