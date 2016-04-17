using Windows.UI.Xaml.Controls;

using GrovePi;
using GrovePi.Sensors;

using jcTM.PCL.Handlers;

namespace jcTM.UWP.IoT {

    public sealed partial class MainPage : Page {
        public MainPage() {
            this.InitializeComponent();

            ReadTemperature();         
        }

        private async void ReadTemperature() {
            var tempHandler = new TemperatureHandler();

            while (true) {
                var temperature = DeviceFactory.Build.TemperatureSensor(Pin.AnalogPin0, TemperatureSensorModel.OnePointTwo).TemperatureInCelcius();

                var convertedTemperature = ((9.0/5.0)*temperature) + 32;

                tempHandler.RecordTemperature(convertedTemperature);    
                
                await System.Threading.Tasks.Task.Delay(5000);
            }
        }
    }
}