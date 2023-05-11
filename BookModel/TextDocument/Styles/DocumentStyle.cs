using System.Diagnostics;
using BookModel.TextDocument.StyleDefinitions;

namespace BookModel.TextDocument.Styles;

public record DocumentStyle
{
    public float SpacingAbove;
    public float SpacingBelow;
    public float LineSpacing;
    public CharacterStyle Character;
    public ListStyle ListStyle;
}

public static class DocumentStyleExt
{
    public static DocumentStyle Apply(this DocumentStyle ds, StyleDefinition definition) =>
        definition switch {
            VerticalSpacingStyleDefinition vsd => ds.ApplyStyleDefinition(vsd),
            ParagraphStyleDefinition psd => ds.ApplyStyleDefinition(psd),
            CharacterStyleDefinition csd => ds.ApplyStyleDefinition(csd),
            ListStyleDefinition lsd => ds.ApplyStyleDefinition(lsd),
            _ => throw new UnreachableException(),
        };

    private static DocumentStyle ApplyStyleDefinition(this DocumentStyle ds,
        VerticalSpacingStyleDefinition definition) =>
        ds with {
            SpacingAbove = definition.Above.IfNone(ds.SpacingAbove)
        };

    private static DocumentStyle ApplyStyleDefinition(this DocumentStyle ds,
        ParagraphStyleDefinition definition) =>
        ds with {
            LineSpacing = definition.LineSpacing.IfNone(ds.LineSpacing),
            SpacingBelow = definition.SpacingBelow.IfNone(ds.SpacingBelow),
            SpacingAbove = definition.SpacingAbove.IfNone(ds.SpacingAbove),
            Character = ApplyCharacterStyleDefinition(ds, definition.Character),
        };

    private static DocumentStyle ApplyStyleDefinition(this DocumentStyle ds,
        CharacterStyleDefinition definition) =>
        ds with {
            Character = ApplyCharacterStyleDefinition(ds, definition)
        };

    private static CharacterStyle ApplyCharacterStyleDefinition(DocumentStyle ds, CharacterStyleDefinition definition)
    {
        return ds.Character with {
            FontFamily = definition.FontFamily.IfNone(ds.Character.FontFamily),
            FontSize = definition.FontSize.IfNone(ds.Character.FontSize),
        };
    }

    private static DocumentStyle ApplyStyleDefinition(this DocumentStyle ds,
        ListStyleDefinition definition) =>
        ds with {
            ListStyle = ds.ListStyle with {
                Indentation = definition.Indentation.IfNone(ds.ListStyle.Indentation),
            },
        };
}