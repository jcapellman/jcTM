using System;
using System.Threading.Tasks;

using Windows.UI.Xaml;
using jcTM.PCL.Handlers;
using jcTM.PCL.Transports;

namespace jcTM.UWP.ViewModels {
    public class MainPageModel : BaseModel {
        private string _recordedTime;

        public string RecordedTime {
            get { return _recordedTime; }
            set { _recordedTime = value; OnPropertyChanged(); }
        }

        private string _temperature;

        public string Temperature {
            get { return _temperature; }
            set { _temperature = value; OnPropertyChanged(); }
        }

        private double _lowTemperature;

        public double LowTemperature {  get { return _lowTemperature; } set { _lowTemperature = value; OnPropertyChanged(); } }

        private double _highTemperature;

        public double HighTemperature { get { return _highTemperature; } set { _highTemperature = value; OnPropertyChanged(); } }

        private Visibility _showProgress;

        public Visibility ShowProgress {
            get {  return _showProgress; }
            set { _showProgress = value; OnPropertyChanged(); }
        }
        
        public async Task<bool> LoadData() {
            ShowProgress = Visibility.Visible;

            var temperatureHandler = new TemperatureHandler();

            var result = await temperatureHandler.GetDashboard();

            if (result == default(DashboardResponseItem)) {
                return false;
            }

            RecordedTime = $"As of {result.Latest_RecordedTime.AddHours(-4)}";
            Temperature = $"{Math.Round(result.Latest_Temperature, 2)}'F";
            HighTemperature = result.CurrentDay_HighTemperature;
            LowTemperature = result.CurrentDay_LowTemperature;

            ShowProgress = Visibility.Collapsed;
            
            return true;
        }
    }
}