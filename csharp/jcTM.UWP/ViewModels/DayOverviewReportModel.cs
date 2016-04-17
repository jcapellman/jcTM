using System.Collections.Generic;
using System.Threading.Tasks;

using Windows.UI.Xaml;

using jcTM.PCL.Handlers;
using jcTM.PCL.Transports;

namespace jcTM.UWP.ViewModels {
    public class DayOverviewReportModel : BaseModel {
        private Visibility _showProgress;

        public Visibility ShowProgress {
            get { return _showProgress; }
            set { _showProgress = value; OnPropertyChanged(); }
        }

        private bool _enableListView;

        public bool EnableListView {
            get { return _enableListView; } set { _enableListView = value; OnPropertyChanged(); }
        }

        private DayOverviewListingResponseItem _selectedListItem;

        public DayOverviewListingResponseItem SelectedListItem {
            get {  return _selectedListItem; } set { _selectedListItem = value; OnPropertyChanged(); }
        }

        private List<DayOverviewListingResponseItem> _listingItems;

        public List<DayOverviewListingResponseItem> ListingItems {
            get { return _listingItems; }
            set { _listingItems = value; OnPropertyChanged(); }
        }

        public async Task<bool> LoadData() {
            EnableListView = false;

            GraphVisibility = Visibility.Collapsed;

            var temperatureHandler = new TemperatureHandler();

            ListingItems = await temperatureHandler.GetDayOverviewListing();

            EnableListView = true;

            return true;
        }

        private List<DayOverviewDetailResponseItem> _detailGraphItems;

        public List<DayOverviewDetailResponseItem> DetailGraphItems {
            get {  return _detailGraphItems; } set { _detailGraphItems = value; OnPropertyChanged(); }
        }

        private Visibility _graphVisibility;

        public Visibility GraphVisibility {
            get { return _graphVisibility; } set { _graphVisibility = value; OnPropertyChanged(); }
        }

        public async Task<bool> LoadGraphData() {
            EnableListView = false;

            var dateTime = $"{SelectedListItem.Day.Month}/{SelectedListItem.Day.Day}/{SelectedListItem.Day.Year}";

            var temperatureHandler = new TemperatureHandler();

            DetailGraphItems = await temperatureHandler.GetDayOverviewGraph(dateTime);

            GraphVisibility = Visibility.Visible;

            EnableListView = true;
            return true;
        }
    }
}