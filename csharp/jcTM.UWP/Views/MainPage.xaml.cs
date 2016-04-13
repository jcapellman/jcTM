using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using jcTM.UWP.ViewModels;

namespace jcTM.UWP.Views {
    public sealed partial class MainPage : Page {
        private MainPageModel viewModel => (MainPageModel) DataContext;

        public MainPage() {
            this.InitializeComponent();

            DataContext = new MainPageModel();
        }
        
        protected override async void OnNavigatedTo(NavigationEventArgs e) {

            while (true) {
                var result = await viewModel.LoadData();

                await System.Threading.Tasks.Task.Delay(5000);
            }
        }
    }
}