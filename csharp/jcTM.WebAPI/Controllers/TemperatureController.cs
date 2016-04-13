using System;
using System.Linq;
using System.Web.Http;

using jcTM.PCL.Transports;
using jcTM.WebAPI.DataLayer.Entities;

namespace jcTM.WebAPI.Controllers {
    [System.Web.Mvc.RoutePrefix("api/Temperature")]
    public class TemperatureController : ApiController {
        public void GET(double temperature) {
            using (var eFactory = new jctmEntities()) {
                var temp = eFactory.Temperatures.Create();

                temp.Active = true;
                temp.Created = DateTime.Now;
                temp.Modified = DateTime.Now;
                temp.Temperature1 = temperature;

                eFactory.Temperatures.Add(temp);
                eFactory.SaveChanges();
            }
        }

        public DashboardResponseItem GET() {
            using (var eFactory = new jctmEntities()) {
                var result = eFactory.WEBAPI_getLatestTemperatureSP().FirstOrDefault();

                if (result == null) {
                    return new DashboardResponseItem();
                }

                return new DashboardResponseItem {
                    Latest_RecordedTime = result.Modified,
                    Latest_Temperature = result.Temperature,
                    CurrentDay_HighTemperature = result.Max.HasValue ? Math.Round(result.Max.Value, 0) : 0.0,
                    CurrentDay_LowTemperature = result.Min.HasValue ? Math.Round(result.Min.Value, 0) : 0.0,
                };
            }
        }
    }
}