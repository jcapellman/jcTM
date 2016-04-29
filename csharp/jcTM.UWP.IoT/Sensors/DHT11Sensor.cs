using GrovePi;
using GrovePi.Sensors;

namespace jcTM.UWP.IoT.Sensors {
    public class DHT11Sensor : BaseTemperatureSensor {
        private ITemperatureSensor _sensor;

        protected override void ConfigureSensor() {
            var device = DeviceFactory.Build.TemperatureSensor(Pin.AnalogPin0, TemperatureSensorModel.OnePointTwo);

            if (device != null) {
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

        protected override double calculateTemperature() => _sensor.TemperatureInCelcius();
    }
}