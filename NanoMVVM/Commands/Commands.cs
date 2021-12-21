using System.Windows.Input;

namespace NanoMVVM;

public static class Commands
{
    public static ICommand None { get; } = new RelayCommand(_ => { }, _ => true);

    public static ICommand CreateCommand() => None;
    public static ICommand CreateCommand(string name) => new RelayCommand(_ => { }, _ => true, name);

    //Execute
    public static ICommand CreateCommand<TParameter>(
        Action<TParameter?> executeAction,
        string? name = default
        ) => new RelayCommand(
            parameter => executeAction(parameter == null ? default : (TParameter)parameter),
            _ => true,
            name);

    public static ICommand CreateCommand(
        Action<object?> executeAction,
        string? name = default
        ) => new RelayCommand(
            executeAction,
            _ => true,
            name);

    public static ICommand CreateCommand(
        Action executeAction,
        string? name = default) => new RelayCommand(
            _ => executeAction(),
            _ => true,
            name);

    public static ICommand CreateCommand(
        Action<object?> executeAction,
        Func<object?, bool> canExecuteFunc,
        string? name = default) => new RelayCommand(executeAction, canExecuteFunc, name);

    public static ICommand CreateCommand<TParameter>(
        Action<TParameter?> executeAction,
        Func<TParameter?, bool> canExecuteFunc,
        string? name = default) => new RelayCommand(
            parameter => executeAction(parameter is null ? default : (TParameter)parameter),
            parameter => canExecuteFunc(parameter is null ? default : (TParameter)parameter),
            name);

    public static ICommand CreateCommand(
        Action executeAction,
        Func<bool> canExecuteFunc,
        string? name = default
        ) => new RelayCommand(
            _ => executeAction(),
            _ => canExecuteFunc(),
            name);
}
