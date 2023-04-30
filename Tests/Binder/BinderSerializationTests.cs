using System.Collections.Immutable;
using BookModel.Binder;
using FluentAssertions;
using LanguageExt.UnitTesting;

namespace Tests.Binder;

public class BinderSerializationTests
{
    [Fact]
    public static void SerializationDeserialization_Recreates_EmptyBinder()
    {
        var binder = new BookBinder();
        var serialized = binder.Serialize();
        var actual = BinderSerialization.Deserialize(serialized);
        actual
            .ShouldBeSuccess(bi =>
                bi.Should()
                    .BeEquivalentTo(binder));
    }

    [Fact]
    public static void SerializationDeserialization_Recreates_BinderWith1Item()
    {
        var root = new Folder {Title = "root folder" , Items = ImmutableList<BinderEntry>.Empty};
        var binder = new BookBinder {Root = root};

        var serialized = binder.Serialize();
        var actual = BinderSerialization.Deserialize(serialized);
        actual
            .ShouldBeSuccess(bi =>
                bi.Should().BeEquivalentTo(binder));
    }
}