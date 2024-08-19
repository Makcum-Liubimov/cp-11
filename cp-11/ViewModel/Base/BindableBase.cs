using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace cp_11.ViewModel.Base;

public class BindableBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected bool Set<T>(ref T? field, T? newValue = default, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field!, newValue!)) return false;

        field = newValue;
        OnPropertyChanged(propertyName);
        return true;
    }
#nullable disable
    protected void OnPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
}