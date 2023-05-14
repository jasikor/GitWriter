using System.Runtime.CompilerServices;
using BookModel.TextDocument;
using BookModel.TextDocument.StyleDefinitions;
using LanguageExt;

[assembly: InternalsVisibleTo("Tests")]

namespace BookModel.Renderers;

public static class DocRenderer
{
    public static dynamic RenderSpan(CharacterSpan characterSpan)
    {
        return new {
            Characters = characterSpan.Characters,
            Class = characterSpan.CharacterStyleDefinitionId.Match(
                d => d.Id, ""),
            Style = characterSpan.CharacterStyle.Match(cs => RenderSpanStyle(cs), "")
        };
    }

    private static dynamic RenderSpanStyle(CharacterStyleDefinition style)
    {
        var res = new {FontFamily = string.Empty, FontSize = string.Empty};
        style.FontFamily.IfSome(ff => { res = res with {FontFamily = ff}; });
        style.FontSize.IfSome(fs => { res = res with {FontSize = fs.ToString()}; });
        return res;
    }
}