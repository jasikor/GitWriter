using FluentAssertions;

namespace Tests.Binder;

public class BinderTests
{
    [Fact]
    public void EmptyBinder_SuccessfullyCreated() => 
        BookModel.Binder.Binder.Create().Should().NotBeNull();
}