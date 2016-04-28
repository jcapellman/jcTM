using Windows.UI.Xaml.Controls;

using GrovePi;
using GrovePi.Sensors;

using jcTM.PCL.Handlers;

namespace jcTM.UWP.IoT {

    public sealed partial class MainPage : Page {
        private ITemperatureSensor _sensor;

        public MainPage() {
            this.InitializeComponent();

            ConfigureSensor();

            ReadTemperature();         
        }

        // Run through the 3 Analog Pins
        private void ConfigureSensor() {
            var device = DeviceFactory.Build.TemperatureSensor(Pin.AnalogPin0, TemperatureSensorModel.OnePointTwo);

            if (device != null)
            {
                _sensor = device;
            }

            device = DeviceFactory.Build.TemperatureSensor(Pin.AnalogPin1, TemperatureSensorModel.OnePointTwo);

            if (device != null) {
                _sensor = device;
            }

            device = DeviceFactory.Build.TemperatureSensor(Pin.AnalogPin2, TemperatureSensorModel.OnePointTwo);

            if (device != null) {
                _sensor = device;
            }
        }

        private async void ReadTemperature() {
            var tempHandler = new TemperatureHandler();

            while (true) {
                var temperature = _sensor.TemperatureInCelcius();

                var convertedTemperature = ((9.0/5.0)*temperature) + 32;

                tempHandler.RecordTemperature(convertedTemperature);    
                
                await System.Threading.Tasks.Task.Delay(5000);
            }
        }
    }
}