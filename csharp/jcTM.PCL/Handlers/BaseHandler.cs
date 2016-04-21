using System;
using System.Net.Http;
using System.Threading.Tasks;

using jcTM.PCL.Global;

using Newtonsoft.Json;

namespace jcTM.PCL.Handlers {
    public abstract class BaseHandler {
        protected abstract string BaseControllerName();

        private HttpClient GetHttpClient() {
            var handler = new HttpClientHandler();

            return new HttpClient(handler) { Timeout = TimeSpan.FromMinutes(1) };
        }

        private string generateURL(string arguments) => string.IsNullOrEmpty(arguments) ? $"{Constants.WEBAPI_ADDRESS}{BaseControllerName()}" : $"{Constants.WEBAPI_ADDRESS}{BaseControllerName()}?{arguments}";

        protected async Task<T> GET<T>(string urlArguments) {
            var str = await GetHttpClient().GetStringAsync(generateURL(urlArguments));

            return JsonConvert.DeserializeObject<T>(str);
        }

        protected async void GET(string urlArguments) {
            await GetHttpClient().GetStringAsync(generateURL(urlArguments));
        }
    }
}