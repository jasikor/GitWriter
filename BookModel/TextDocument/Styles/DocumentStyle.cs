using System.ComponentModel;
using System.Diagnostics;
using BookModel.TextDocument.StyleDefinitions;
using LanguageExt.Pretty;

namespace BookModel.TextDocument.Styles;

public record DocumentStyle
{
    public float SpacingAbove;
    public float SpacingBelow;
    public float LineSpacing;
    public FontStyle Font;
    public ListStyle ListStyle;
}

public static class DocumentStyleExt
{
    public static DocumentStyle Apply(this DocumentStyle ds, StyleDefinition definition) =>
        definition switch {
            VerticalSpacingStyleDefinition vsd => ds.ApplyStyleDefinition(vsd),
            ParagraphStyleDefinition psd => ds.ApplyStyleDefinition(psd),
            FontStyleDefinition fsd => ds.ApplyStyleDefinition(fsd),
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
            Font = ApplyFontStyleDefinition(ds, definition.Font),
        };

    private static DocumentStyle ApplyStyleDefinition(this DocumentStyle ds,
        FontStyleDefinition definition) =>
        ds with {
            Font = ApplyFontStyleDefinition(ds, definition)
        };

    private static FontStyle ApplyFontStyleDefinition(DocumentStyle ds, FontStyleDefinition definition)
    {
        return ds.Font with {
            Family = definition.Family.IfNone(ds.Font.Family),
            Size = definition.Size.IfNone(ds.Font.Size),
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