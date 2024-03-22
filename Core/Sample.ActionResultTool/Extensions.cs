using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ActionResultTool
{
    public static class Extensions
    {
        public static async Task<ActionResult<T>> ToActionResult<T>(this HttpResponseMessage response) where T : class
        {
            if (response.IsSuccessStatusCode)
            {
                var value = await response.Content.ReadJsonContentAsync<T>();
                if (value == null) return ActionResult.Fail;

                return value;
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return ActionResult.FailWithMessage("服务器未找到请求的资源");
            }

            Response? errorResponse = await response.Content.ReadJsonContentAsync<Response>();
            string errorText = errorResponse?.Message ?? (errorResponse?.Error?.Message ?? $"未知错误：{response.StatusCode}");

            return ActionResult.FailWithMessage(errorText);
        }

        public static async Task<ActionResult> ToActionResult(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                Response? responseObj = await response.Content.ReadJsonContentAsync<Response>();
                return ActionResult.SuccessWithMessage(responseObj?.Message);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return ActionResult.FailWithMessage("服务器未找到请求的资源");
            }

            Response? errorResponse = await response.Content.ReadJsonContentAsync<Response>();
            string errorText = errorResponse?.Message ?? (errorResponse?.Error?.Message ?? $"未知错误：{response.StatusCode}");

            return ActionResult.FailWithMessage(errorText);
        }

        public static async Task<ActionResult<T>> SendResult<T>(this HttpClient httpClient, HttpRequestMessage request) where T : class
        {
            HttpResponseMessage response;
            try
            {
                response = await httpClient.SendAsync(request);
            }
            catch (HttpRequestException)
            {
                return ActionResult.FailWithMessage($"网络错误，请检查网络连接");
            }
            catch (Exception)
            {
                return ActionResult.FailWithMessage($"连接异常，请检查网络连接");
            }
            return await response.ToActionResult<T>();
        }
        public static async Task<ActionResult> SendResult(this HttpClient httpClient, HttpRequestMessage request)
        {
            HttpResponseMessage response;
            try
            {
                response = await httpClient.SendAsync(request);
            }
            catch (Exception e)
            {
                return ActionResult.FailWithMessage($"网络错误：\n{e.Message}");
            }
            return await response.ToActionResult();
        }

        private static async Task<ActionResult<T>> SendJson<T>(this HttpClient httpClient, string url, HttpMethod method, object? value = null) where T : class
        {
            HttpRequestMessage request = new HttpRequestMessage(method, url);
            if (value != null)
            {
                request.Content = HttpHelper.JsonContent(value);
            }
            return await httpClient.SendResult<T>(request);
        }
        private static async Task<ActionResult> SendJson(this HttpClient httpClient, string url, HttpMethod method, object? value = null)
        {
            HttpRequestMessage request = new HttpRequestMessage(method, url);
            if (value != null)
            {
                request.Content = HttpHelper.JsonContent(value);
            }
            return await httpClient.SendResult(request);
        }

        public static async Task<ActionResult<T>> PostResult<T>(this HttpClient httpClient, string url, object value) where T : class
            => await httpClient.SendJson<T>(url, HttpMethod.Post, value);

        public static async Task<ActionResult> PostResult(this HttpClient httpClient, string url, object value) =>
            await httpClient.SendJson(url, HttpMethod.Post, value);

        public static async Task<ActionResult<T>> PutResult<T>(this HttpClient httpClient, string url, object value) where T : class =>
            await httpClient.SendJson<T>(url, HttpMethod.Put, value);

        public static async Task<ActionResult> PutResult(this HttpClient httpClient, string url, object value) =>
            await httpClient.SendJson(url, HttpMethod.Put, value);

        public static async Task<ActionResult> DeleteResult(this HttpClient httpClient, string url) =>
            await httpClient.SendJson(url, HttpMethod.Delete);

        public static async Task<ActionResult<T>> DeleteResult<T>(this HttpClient httpClient, string url) where T : class =>
            await httpClient.SendJson<T>(url, HttpMethod.Delete);

        public static async Task<ActionResult> PatchResult(this HttpClient httpClient, string url) =>
    await httpClient.SendJson(url, new HttpMethod("PATCH"));

        public static async Task<ActionResult<T>> PatchResult<T>(this HttpClient httpClient, string url) where T : class =>
            await httpClient.SendJson<T>(url, new HttpMethod("PATCH"));

        public static async Task<ActionResult<T>> GetResult<T>(this HttpClient httpClient, string url) where T : class =>
            await httpClient.SendJson<T>(url, HttpMethod.Get);

        public static async Task<ActionResult> GetResult(this HttpClient httpClient, string url) =>
            await httpClient.SendJson(url, HttpMethod.Get);

    }
}
