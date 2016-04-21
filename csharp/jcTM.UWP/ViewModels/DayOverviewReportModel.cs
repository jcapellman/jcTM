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
            ShowProgress = Visibility.Visible;

            EnableListView = false;

            GraphVisibility = Visibility.Collapsed;

            var dayOverviewHandler = new DayOverviewReportHandler();

            ListingItems = await dayOverviewHandler.GetDayOverviewListing();

            EnableListView = true;

            ShowProgress = Visibility.Collapsed;

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
            ShowProgress = Visibility.Visible;

            EnableListView = false;
            
            var dayOverviewHandler = new DayOverviewReportHandler();

            DetailGraphItems = await dayOverviewHandler.GetDayOverviewGraph(SelectedListItem.ID);

            GraphVisibility = Visibility.Visible;

            EnableListView = true;

            ShowProgress = Visibility.Collapsed;

            return true;
        }
    }
}