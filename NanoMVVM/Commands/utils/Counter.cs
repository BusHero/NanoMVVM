namespace NanoMVVM;

internal class Counter
{
    private int _currentValue;

    public int CurrentValue => _currentValue;

    public Counter(int startValue = 0) => _currentValue = startValue;

    public int Next() => Interlocked.Increment(ref _currentValue);
}
