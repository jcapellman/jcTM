namespace jcTM.PCL.Handlers {
    public class TemperatureHandler : BaseHandler {        
        public void RecordTemperature(double temperature) { GET($"Temperature?temperature={temperature}"); }
    }
}