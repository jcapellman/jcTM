using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace jcTM.Mobile.ViewModels {
    public class BaseModel : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }    
    }
}