using jcTM.Mobile.ViewModels;

using Xamarin.Forms;

namespace jcTM.Mobile {
    public partial class MainPage : ContentPage {
        public MainPageModel viewModel => (MainPageModel)BindingContext;

        public MainPage() {
            InitializeComponent();

            BindingContext = new MainPageModel();
        }

        protected override async void OnBindingContextChanged() {
            var result = await viewModel.LoadData();

            lTemperature.Text = viewModel.Temperature;
            lRecordedTime.Text = viewModel.RecordedTime;

            lLowTemperature.Text = viewModel.LowTemperature.ToString();
            lHighTemperature.Text = viewModel.HighTemperature.ToString();
        }
    }
}