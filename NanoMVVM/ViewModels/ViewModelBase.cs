using NanoMVVM.Utils;

using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NanoMVVM;


public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public ViewModelBase()
    {
        PropertyChanged += HandlePropertyChangedEvent;
    }

    protected virtual void HandlePropertyChangedEvent(
        object? sender,
        PropertyChangedEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e);

        if (e.PropertyName is null)
            return;

        if (false == HandlersDictionary.TryGetValue(e.PropertyName, out var list))
            return;

        foreach (var (foo, _) in list.Where(tuple => tuple.Item2(sender, e)))
        {
            foo?.Invoke(sender, e);
        }
    }

    protected void FirePropertyChangedEvent(
        [CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new (propertyName));

    protected virtual bool Set<T>(
        ref T storage,
        T value,
        [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(storage, value))
            return false;
        storage = value;
        FirePropertyChangedEvent(propertyName);
        return true;
    }

    protected virtual bool Set<T>(object source, T value, string propertyName)
    {
        var property = source.GetType()
            .GetRuntimeProperties()
            .FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase));
        
        if (property is null)
            return false;
        if (property.GetValue(source) is not T castedActualValue)
            return false;
        if (EqualityComparer<T>.Default.Equals(castedActualValue, value))
            return false;

        property.SetValue(source, value);
        FirePropertyChangedEvent(propertyName);
        
        return true;
    }

    #region SubscribePropertyChanged

    public virtual ViewModelBase SubscribePropertyChanged(
        string propertyName,
        Action eventHandler) => SubscribePropertyChanged(propertyName, (_, __) => eventHandler(), Funcs.AlwaysTrue<object?, PropertyChangedEventArgs>);

    public virtual ViewModelBase SubscribePropertyChanged(
        string propertyName,
        PropertyChangedEventHandler eventHandler) => SubscribePropertyChanged(propertyName, eventHandler, Funcs.AlwaysTrue<object?, PropertyChangedEventArgs>);

    public virtual ViewModelBase SubscribePropertyChanged(
        string propertyName,
        Action eventHandler,
        Func<bool> canChange) => SubscribePropertyChanged(propertyName, (_, __) => eventHandler(), (_, __) => canChange());

    public virtual ViewModelBase SubscribePropertyChanged(
        string propertyName,
        Action eventHandler,
        CanNotifyPropertyChangedEventHandler canChange) => SubscribePropertyChanged(propertyName, (_, __) => eventHandler(), canChange);

    public virtual ViewModelBase SubscribePropertyChanged(
        string propertyName,
        PropertyChangedEventHandler eventHandler,
        Func<bool> canChange) => SubscribePropertyChanged(propertyName, eventHandler, (_, __) => canChange());

    public virtual ViewModelBase SubscribePropertyChanged(
        string propertyName,
        PropertyChangedEventHandler eventHandler,
        CanNotifyPropertyChangedEventHandler canChange)
    {
        ArgumentNullException.ThrowIfNull(propertyName);
        ArgumentNullException.ThrowIfNull(eventHandler);
        ArgumentNullException.ThrowIfNull(canChange);

        var list = HandlersDictionary.GetValueOrSet(propertyName, () => new());
        list.Add(new(eventHandler, canChange));

        return this;
    }

    #endregion

    #region Private Fields

    private Dictionary<string, List<Handler>> HandlersDictionary { get; } = new ();

    #endregion

    private record struct Handler(PropertyChangedEventHandler PropertyChangedEventHandler, CanNotifyPropertyChangedEventHandler Item2);
}




