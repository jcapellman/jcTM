using System.Threading.Tasks;

using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

using Template10.Controls;

namespace jcTM.UWP {
    sealed partial class App : Template10.Common.BootStrapper {
        public App() {
            InitializeComponent();
        }

        public override async Task OnInitializeAsync(IActivatedEventArgs args) {
            if (Window.Current.Content as ModalDialog == null) {
                // create a new frame 
                var nav = NavigationServiceFactory(BackButton.Attach, ExistingContent.Include);
                // create modal root
                Window.Current.Content = new ModalDialog {
                    DisableBackButtonWhenModal = true,
                    Content = new jcTM.UWP.Views.Shell(nav),
                    ModalContent = new jcTM.UWP.Views.Busy(),
                };
            }
            await Task.CompletedTask;
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args) {
            // long-running startup tasks go here

            NavigationService.Navigate(typeof(jcTM.UWP.Views.MainPage));
            await Task.CompletedTask;
        }
    }
}