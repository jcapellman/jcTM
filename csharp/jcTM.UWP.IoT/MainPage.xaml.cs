using Windows.UI.Xaml.Controls;

using jcTM.PCL.Handlers;
using jcTM.UWP.IoT.Sensors;

namespace jcTM.UWP.IoT {

    public sealed partial class MainPage : Page {
        public MainPage() {
            this.InitializeComponent();
            
            ReadTemperature();         
        }
        
        private async void ReadTemperature() {
            var tempHandler = new TemperatureHandler();

            var temperatureSensor = new DHT11Sensor();

            while (true) {
                tempHandler.RecordTemperature(temperatureSensor.GetTemperature(false));    
                
                await System.Threading.Tasks.Task.Delay(5000);
            }
        }
    }
}