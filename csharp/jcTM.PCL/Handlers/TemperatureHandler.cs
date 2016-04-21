using System.Threading.Tasks;

using jcTM.PCL.Transports;

namespace jcTM.PCL.Handlers {
    public class TemperatureHandler : BaseHandler {
              
        public void RecordTemperature(double temperature) { GET($"temperature={temperature}"); }

        public async Task<DashboardResponseItem> GetDashboard() => await GET<DashboardResponseItem>(string.Empty);
        
        protected override string BaseControllerName() => "Temperature";
    }
}