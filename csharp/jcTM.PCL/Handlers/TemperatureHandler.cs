using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using jcTM.PCL.Transports;

namespace jcTM.PCL.Handlers {
    public class TemperatureHandler : BaseHandler {        
        public void RecordTemperature(double temperature) { GET($"Temperature?temperature={temperature}"); }

        public async Task<DashboardResponseItem> GetDashboard() => await GET<DashboardResponseItem>("Temperature");

        public async Task<List<DayOverviewListingResponseItem>> GetDayOverviewListing() => await GET<List<DayOverviewListingResponseItem>>("DayOverviewReport");

        public async Task<List<DayOverviewDetailResponseItem>> GetDayOverviewGraph(string dateTime) => 
            await GET<List<DayOverviewDetailResponseItem>>($"DayOverviewReport?selectedDay={dateTime}");
    }
}