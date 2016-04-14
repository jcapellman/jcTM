using System;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using jcTM.PCL.Global;

using Newtonsoft.Json;

namespace jcTM.UWP.ViewModels {
    public class BaseModel : INotifyPropertyChanged {
        public async Task<T> GET<T>(string urlArguments) {
            var handler = new HttpClientHandler();
            var client = new HttpClient(handler) { Timeout = TimeSpan.FromMinutes(1) };
            var str = await client.GetStringAsync($"{Constants.WEBAPI_ADDRESS}{urlArguments}");

            return JsonConvert.DeserializeObject<T>(str);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
    }
}