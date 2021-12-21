using System.Windows.Input;

namespace NanoMVVM;

internal class RelayCommand : ICommand
{
    public string Name { get; }

    public RelayCommand(Action<object?> execute, Func<object?, bool> canExecute, string? name = default)
    {
        ExecuteAction = execute ?? throw new ArgumentNullException(nameof(execute));
        CanExecuteFunc = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        Name = name ?? $"{nameof(RelayCommand)}.{Counter.Next()}";
    }

    private Action<object?> ExecuteAction { get; }
    private Func<object?, bool> CanExecuteFunc { get; }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => CanExecuteFunc(parameter);

    public void Execute(object? parameter) => ExecuteAction(parameter);

    private static Counter Counter { get; } = new Counter();
}
