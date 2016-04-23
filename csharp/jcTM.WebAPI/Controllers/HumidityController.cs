using jcTM.WebAPI.DataLayer.Entities;

namespace jcTM.WebAPI.Controllers {
    public class HumidityController : BaseController {
        public void GET(double humidity) {
            using (var eFactory = new jctmEntities()) {
                var humidityItem = eFactory.Humidities.Create();

                humidityItem.Humidity1 = humidity;

                eFactory.Humidities.Add(humidityItem);
                eFactory.SaveChanges();
            }
        }
    }
}