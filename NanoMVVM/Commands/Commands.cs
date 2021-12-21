using System.Windows.Input;

namespace NanoMVVM;

/// <summary>
/// A utility class to simplify the creation of commands
/// </summary>
public static class Commands
{
    #region Private stuff

    static Commands()
    {
        Counter = new Counter();
        None = new RelayCommand(DoNothing, AlwaysReturnTrue, CrateProgramName(default));
    }

    private static Counter Counter { get; }

    private static ICommand None { get; }
    
    private static void DoNothing(object? _) { }

    private static bool AlwaysReturnTrue(object? _) => true;

    private static string CrateProgramName(string? name) => name ?? $"{nameof(RelayCommand)}.{Counter.Next()}";
    
    #endregion

    /// <summary>
    /// Creates a command that does nothing.
    /// </summary>
    /// <returns>A command that does nothing</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Null_object_pattern"/>
    public static ICommand CreateCommand() => None;

    /// <summary>
    /// Creates a command that does nothing.
    /// </summary>
    /// <param name="name">A name for the command</param>
    /// <returns>A command with the specified name that does nothing.</returns>
    public static ICommand CreateCommand(string name) => new RelayCommand(DoNothing, AlwaysReturnTrue, name);

    /// <inheritdoc cref="CreateCommand(Action{object?}, Func{object?, bool}, string?)"/>
    public static ICommand CreateCommand<TParameter>(
        Action<TParameter?> executeAction,
        string? name = default
        ) => new RelayCommand(
            parameter => executeAction(parameter == null ? default : (TParameter)parameter),
            AlwaysReturnTrue,
            CrateProgramName(name));

    /// <inheritdoc cref="CreateCommand(Action{object?}, Func{object?, bool}, string?)"/>
    public static ICommand CreateCommand(
        Action<object?> executeAction,
        string? name = default
        ) => new RelayCommand(
            executeAction,
            AlwaysReturnTrue,
            CrateProgramName(name));

    /// <inheritdoc cref="CreateCommand(Action{object?}, Func{object?, bool}, string?)"/>
    public static ICommand CreateCommand(
        Action executeAction,
        string? name = default) => new RelayCommand(
            _ => executeAction(),
            AlwaysReturnTrue,
            CrateProgramName(name));

    /// <summary>
    /// Creates a command that performs the action specified by the executeAction parameter.
    /// </summary>
    /// <param name="executeAction">The action to be ivnoked when the command is executed.</param>
    /// <param name="canExecuteFunc">The predicate to be invoked when determining whether to execute a command or not.</param>
    /// <param name="name">An optional parameter to specify the name of the command. If no name is specified, the method will generate a proper name.</param>
    /// <returns>A command that will perform the specified action when executed.</returns>
    public static ICommand CreateCommand(
        Action<object?> executeAction,
        Func<object?, bool> canExecuteFunc,
        string? name = default) => new RelayCommand(
            executeAction,
            canExecuteFunc,
            CrateProgramName(name));

    /// <inheritdoc cref="CreateCommand(Action{object?}, Func{object?, bool}, string?)"/>
    public static ICommand CreateCommand<TParameter>(
        Action<TParameter?> executeAction,
        Func<TParameter?, bool> canExecuteFunc,
        string? name = default) => new RelayCommand(
            parameter => executeAction(parameter is null ? default : (TParameter)parameter),
            parameter => canExecuteFunc(parameter is null ? default : (TParameter)parameter),
            CrateProgramName(name));

    /// <inheritdoc cref="CreateCommand(Action{object?}, Func{object?, bool}, string?)"/>
    public static ICommand CreateCommand(
        Action executeAction,
        Func<bool> canExecuteFunc,
        string? name = default
        ) => new RelayCommand(
            _ => executeAction(),
            _ => canExecuteFunc(),
            CrateProgramName(name));


}
