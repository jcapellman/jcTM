using System;

using Windows.UI.Xaml.Controls;

using jcTM.PCL.Handlers;
using jcTM.UWP.IoT.Sensors;

using Microsoft.Azure.Devices.Client;

namespace jcTM.UWP.IoT {

    public sealed partial class MainPage : Page {
        public MainPage() {
            this.InitializeComponent();
            
            ReadTemperature();         
        }
        
        private async void ReadTemperature() {
            var tempHandler = new TemperatureHandler();

            var temperatureSensor = new DHT11Sensor();

            var _deviceClient = DeviceClient.CreateFromConnectionString("iottemp.azure-devices.net", TransportType.Http1);
            
            while (true) {
                var receivedMessage = await _deviceClient.ReceiveAsync();

                if (receivedMessage != null) {
                    await _deviceClient.CompleteAsync(receivedMessage);
                }

                tempHandler.RecordTemperature(temperatureSensor.GetTemperature(false));

                await System.Threading.Tasks.Task.Delay(5000);
            }
        }
    }
}