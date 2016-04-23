using System;

namespace jcTM.PCL.Handlers {
    public class HumidityHandler : BaseHandler {
        protected override string BaseControllerName() => "Humidity";

        public void RecordHumidity(double humidity) {
            GET($"humidity={humidity}");
        }
    }
}