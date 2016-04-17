using System.Web.Http;

using jcTM.WebAPI.DataLayer.Entities;

namespace jcTM.WebAPI.Controllers {
    [System.Web.Mvc.RoutePrefix("api/NightlyJob")]
    public class NightlyJobController : ApiController {
        public void GET() {
            using (var eFactory = new jctmEntities()) {
                eFactory.Database.ExecuteSqlCommand("EXEC dbo.SQL_runNightlyJobsSP");
            }
        }
    }
}