using System.ComponentModel;

namespace NanoMVVM;

public class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
}

public class ViewModelBase<T>: ViewModelBase where T : ViewModelBase<T>
{

}
