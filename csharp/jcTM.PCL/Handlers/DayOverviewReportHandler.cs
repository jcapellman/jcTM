using System.Collections.Generic;
using System.Threading.Tasks;

using jcTM.PCL.Transports;

namespace jcTM.PCL.Handlers {
    public class DayOverviewReportHandler : BaseHandler {
        protected override string BaseControllerName() => "DayOverviewReport";

        public async Task<List<DayOverviewListingResponseItem>> GetDayOverviewListing() => await GET<List<DayOverviewListingResponseItem>>("DayOverviewReport");

        public async Task<List<DayOverviewDetailResponseItem>> GetDayOverviewGraph(int selectedDayListingID) =>
            await GET<List<DayOverviewDetailResponseItem>>($"DayOverviewReport?selectedDayListingID={selectedDayListingID}");

    }
}