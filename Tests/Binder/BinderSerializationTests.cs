using System.Collections.Immutable;
using BookModel.Binder;
using FluentAssertions;
using LanguageExt.UnitTesting;

namespace Tests.Binder;

public class BinderSerializationTests
{
    [Fact]
    public static void SerializationDeserialization_Recreates_Empty_Binder()
    {
        var binder = new BookBinder();
        var actual = BinderSerialization.Deserialize(binder.Serialize());
        actual
            .ShouldBeSuccess(bi => 
                bi.Should()
                    .BeEquivalentTo(binder, o => o.IncludingInternalFields().WithStrictOrdering()));
    }
}

