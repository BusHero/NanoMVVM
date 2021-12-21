# NanoMVVM

A small collection of stuff I use across WPF projects.

## ViewModelBase/ViewModelBase<T>

The base class for all view models.
Use the generic version.
Implements 

Usage example:

```c#
public class MyViewModel : ViewModelBase<MyViewModel>
{
	private int foo;

	public int Foo { get => foo; set => Set(ref foo, value); }

	public MyViewModel
	{
		SubscribePropertyChanged(nameof(Foo), () => { /* Do something */ });
		SubscribePropertyChanged(nameof(Foo), (MyViewModel myViewModel) => { /* Do something */});
		SubscribePropertyChanged(nameof(Foo), HandleFooChanged);
		SubscribePropertyChanged(nameof(Foo), HandleFooChangedWithArgument);
	}

	private void HandleFooChanged()
	{
		// Do something
	}

	private void HandleFooChangedWithArgument(MyViewModel myViewModel)
	{
		// Do something
	}
}
```

## Commands

Package provide utility methods to create custom commands through the Commands utility class.
The method through which a command is created is CreateCommand(...).
It has a number of overrides to create command in a variaty of scenarios.

Examples:
```c#

ICommand command = Commands.CreateCommand(() => { /* Do something ignoring the argument */});
ICommand commandWithObjectArgument = Commands.CreateCommand((object arg) => { /* Do something */});
ICommand commandWithStronglyTypedArgument = Commands.CreateCommand<T>((T arg) => { /* Do something */});

```