namespace NanoMVVM;

public abstract class ViewModelBase<T> : ViewModelBase
    where T : ViewModelBase<T>
{
    public virtual T? SubscribePropertyChanged(
        string propertyName,
        Action<T> eventHandler) => SubscribePropertyChanged(propertyName, (sender, _) => { eventHandler(sender); });

    public virtual T? SubscribePropertyChanged(
        string propertyName,
        PropertyChangedEventHandler<T> eventHandler)
    {
        var foo = base.SubscribePropertyChanged(propertyName, (sender, eventArgs) =>
        {
            if (sender is T t)
                eventHandler(t, eventArgs);
        });
        return foo as T;
    }
}
