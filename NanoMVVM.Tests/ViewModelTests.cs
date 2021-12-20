
namespace NanoMVVM.Tests;

public class ViewModelTests
{
    [Fact]
    public void ModifiingPropertyRaisesEvent()
    {
        var viewModel = new ViewModelStub();
        var monitor = viewModel.Monitor();

        viewModel.Property = true;
        viewModel.Property.Should().Be(true);
        monitor.Should().RaisePropertyChangeFor(x => x.Property);
    }

    [Fact]
    public void EventIsRaisedOnlyWhenANewValueIsSetUp()
    {
        var viewModel = new ViewModelStub();
        var monitor = viewModel.Monitor();

        viewModel.Property = false;
        viewModel.Property.Should().Be(false);
        monitor.Should().NotRaisePropertyChangeFor(x => x.Property);
    }
}

public class ViewModelStub : ViewModelBase<ViewModelStub>
{
    private bool property;

    public bool Property { get => property; set => Set(ref property, value); }
}