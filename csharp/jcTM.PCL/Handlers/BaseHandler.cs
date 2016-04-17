using System;
using System.Net.Http;
using System.Threading.Tasks;

using jcTM.PCL.Global;

using Newtonsoft.Json;

namespace jcTM.PCL.Handlers {
    public class BaseHandler {
        protected async Task<T> GET<T>(string urlArguments) {
            var handler = new HttpClientHandler();
            var client = new HttpClient(handler) { Timeout = TimeSpan.FromMinutes(1) };
            var str = await client.GetStringAsync($"{Constants.WEBAPI_ADDRESS}{urlArguments}");

            return JsonConvert.DeserializeObject<T>(str);
        }

        protected async void GET(string urlArguments) {
            var handler = new HttpClientHandler();
            var client = new HttpClient(handler) { Timeout = TimeSpan.FromMinutes(1) };
            await client.GetStringAsync($"{Constants.WEBAPI_ADDRESS}{urlArguments}");
        }
    }
}