using Windows.UI.Xaml.Controls;

using GrovePi;
using GrovePi.Sensors;

using jcTM.PCL.Handlers;

namespace jcTM.UWP.IoT {

    public sealed partial class MainPage : Page {
        private const bool USING_DHT11 = true;

        public MainPage() {
            this.InitializeComponent();

            ReadTemperature();         
        }

        private async void ReadTemperature() {
            var tempHandler = new TemperatureHandler();
            var humidityHandler = new HumidityHandler();

            while (true) {
                if (USING_DHT11) {
                    var temperature = DeviceFactory.Build.TemperatureSensor(Pin.AnalogPin0, TemperatureSensorModel.OnePointTwo).TemperatureInCelcius();

                    var convertedTemperature = ((9.0 / 5.0) * temperature) + 32;

                    tempHandler.RecordTemperature(convertedTemperature);
                } else {
                    var response = DeviceFactory.Build.TemperatureAndHumiditySensor(Pin.AnalogPin1, TemperatureAndHumiditySensorModel.DHT22).TemperatureAndHumidity();
                    
                    var convertedTemperature = ((9.0 / 5.0) * response.Temperature) + 32;

                    tempHandler.RecordTemperature(convertedTemperature);

                    humidityHandler.RecordHumidity(response.Humidity);
                }
                
                await System.Threading.Tasks.Task.Delay(5000);
            }
        }
    }
}