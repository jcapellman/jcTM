using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using jcTM.UWP.ViewModels;

namespace jcTM.UWP.Views {

    public sealed partial class DayOverviewReport : Page {
        private DayOverviewReportModel viewModel => (DayOverviewReportModel) DataContext;

        public DayOverviewReport() {
            this.InitializeComponent();

            DataContext = new DayOverviewReportModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e) { var result = await viewModel.LoadData(); }

        private async void lstItems_SelectionChanged(object sender, SelectionChangedEventArgs e) { await viewModel.LoadGraphData(); }
    }
}