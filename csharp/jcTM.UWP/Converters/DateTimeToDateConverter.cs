using System;

using Windows.UI.Xaml.Data;

namespace jcTM.UWP.Converters {
    public class DateTimeToDateConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string culture) {
            return ((DateTime)value).ToString("MM/dd/yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture) {
            throw new NotImplementedException();
        }
    }
}