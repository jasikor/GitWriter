
using BookModel;
using BookModel.Binder;
using FluentAssertions;

namespace Tests;

public class BinderTests
{
    [Fact]
    public void EmptyBinder_SuccessfullyCreated() => 
        Binder.Create().Should().NotBeNull();
}