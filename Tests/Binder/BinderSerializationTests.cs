using BookModel.Binder;
using FluentAssertions;
using LanguageExt.UnitTesting;
using static BookModel.Binder.BinderSerialization;

namespace Tests.Binder;

public class BinderSerializationTests
{
    [Fact]
    public static void SerializationDeserialization_Recreates_EmptyBinder()
    {
        var binder = new BookBinder();
        TestSerializeDeserialize(binder);
    }

    [Fact]
    public static void SerializationDeserialization_Recreates_BinderWith1Item()
    {
        var root = new Folder("root folder");
        var binder = new BookBinder {Root = root};

        TestSerializeDeserialize(binder);
    }

    private static void TestSerializeDeserialize(BookBinder binder)
    {
        var serialized = binder.Serialize();
        var actual = Deserialize(serialized);
        actual.ShouldBeSuccess(bi =>
                bi.Should().BeEquivalentTo(binder));
    }
}