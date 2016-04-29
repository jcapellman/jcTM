namespace jcTM.UWP.IoT.Sensors {
    public abstract class BaseTemperatureSensor {
        public double GetTemperature(bool returnCelsius) {
            ConfigureSensor();

            return convertTemperature(calculateTemperature(), returnCelsius);
        }

        protected abstract double calculateTemperature();

        protected abstract void ConfigureSensor();

        private double convertTemperature(double temperature, bool returnCelsius = false) {
            if (returnCelsius) {
                return temperature;
            }

            return ((9.0 / 5.0) * temperature) + 32;
        }
    }
}