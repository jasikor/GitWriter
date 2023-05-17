using System.Diagnostics;
using BookModel.TextDocument.StyleDefinitions;
using LanguageExt;

namespace BookModel.TextDocument.Styles;

public record DocumentStyle
{
    public float SpacingAbove;
    public float SpacingBelow;
    public float LineSpacing;
    public CharacterStyle CharacterStyle = new ();
    public ListStyle ListStyle = new();
}

public static class DocumentStyleExt
{
    public static DocumentStyle ApplyStyleDefinition(this DocumentStyle ds,
        Option<VerticalSpacingStyleDefinition> definition) =>
        definition
            .Match(
                vs => ds with {
                    SpacingAbove = vs.Above.IfNone(ds.SpacingAbove)
                },
                ds);

    public static DocumentStyle ApplyStyleDefinition(this DocumentStyle ds,
        Option<ParagraphStyleDefinition> definition) =>
        definition
            .Match(
                de =>
                    ds with {
                            LineSpacing = de.LineSpacing.IfNone(ds.LineSpacing),
                            SpacingBelow = de.SpacingBelow.IfNone(ds.SpacingBelow),
                            SpacingAbove = de.SpacingAbove.IfNone(ds.SpacingAbove),
                            CharacterStyle = ApplyCharacterStyleDefinitionReturnCharacterStyle(ds.CharacterStyle, de.Character),
                        },
                ds);

    private static CharacterStyle ApplyCharacterStyleDefinitionReturnCharacterStyle(
        CharacterStyle style, 
        Option<CharacterStyleDefinition> styleDefinition) =>
        styleDefinition.Match(sd =>
                style with {
                    FontSize = sd.FontSize.IfNone(style.FontSize),
                    FontFamily = sd.FontFamily.IfNone(style.FontFamily),
                },
            style);

    public static DocumentStyle ApplyStyleDefinition(this DocumentStyle ds,
        Option<CharacterStyleDefinition> definition) =>
        definition
            .Match(de =>
                    ds with {
                        CharacterStyle = new CharacterStyle() {
                            FontFamily = de.FontFamily.IfNone(ds.CharacterStyle.FontFamily),
                            FontSize = de.FontSize.IfNone(ds.CharacterStyle.FontSize),
                        }
                    },
                ds);

    public static DocumentStyle ApplyStyleDefinition(this DocumentStyle ds,
        Option<ListStyleDefinition> definition) =>
        definition
            .Match(
                de =>
                    ds with {
                        ListStyle = ds.ListStyle with {
                            Indentation = de.Indentation.IfNone(ds.ListStyle.Indentation),
                        },
                    },
                ds);
}