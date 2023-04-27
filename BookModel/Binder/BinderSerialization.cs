using System.ComponentModel.Design.Serialization;
using System.Text.Json;
using BookModel.Document;
using LanguageExt;
using LanguageExt.Common;

namespace BookModel.Binder;

public static class BinderSerialization
{
    public static string Serialize(this BookBinder @this) =>
        JsonSerializer.Serialize(@this, new JsonSerializerOptions { WriteIndented = true });

    public static Try<BookBinder> Deserialize(string serialized) => 
        () => JsonSerializer.Deserialize<BookBinder>(serialized);
}