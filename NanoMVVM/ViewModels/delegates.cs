using System.ComponentModel;

namespace NanoMVVM;

public delegate bool CanNotifyPropertyChangedEventHandler(object? sender, PropertyChangedEventArgs e);

public delegate void PropertyChangedEventHandler<T>(T sender, PropertyChangedEventArgs e);
