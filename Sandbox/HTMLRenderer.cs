using System.Diagnostics;
using System.Text;
using BookModel.Renderers;
using BookModel.TextDocument;
using BookModel.TextDocument.Styles;
using Stubble.Core.Builders;

namespace Sandbox;

public class HTMLRenderer
{
    private readonly IStylesManager _stylesManager;

    public HTMLRenderer(IStylesManager stylesManager)
    {
        _stylesManager = stylesManager;
    }

    public StringBuilder Render(Document doc, DocumentStyle style)
    {
        var docHead = $"<!DOCTYPE html><html> <head><title>{doc.Title}</title></head>\n" +
                      $"<body  style=\"{GetFontStyle(style)}\">\n";

        var sections = doc.Items
            .Select(d => RenderDocSection(d, style))
            .Fold(new StringBuilder(), (a, sb) => a.Append(sb));

        var docFoot = "</body>\n</html>";

        return new StringBuilder()
            .Inspect(style.ToString())
            .Append(docHead)
            .Append(sections)
            .Append(docFoot);
    }

    private string GetFontStyle(DocumentStyle style) =>
        $"font-family:{style.CharacterStyle.FontFamily};" +
        $"font-size:{style.CharacterStyle.FontSize};";

    private StringBuilder RenderDocSection(DocumentSection documentSection, DocumentStyle style)
    {
        var s = _stylesManager
            .ApplyStyleId(style, documentSection.ParagraphStyleId)
            .ApplyStyleDefinition(documentSection.VerticalSpacing);

        var head = $"<div style=\"margin: {style.SpacingAbove}px 0px {style.LineSpacing}px \">";

        object content = documentSection switch {
            ParagraphSection par => RenderParagraph(par, s),
            ListSection list => RenderList(list, s),
            _ => new UnreachableException()
        };

        var foot = "</div>\n";

        return new StringBuilder()
            .Inspect($"ParagraphStyleId: {documentSection.ParagraphStyleId.Match(s => s.Id, "None")}")
            .Inspect($"Above: {s.SpacingAbove}")
            .Append(head)
            .Append(content)
            .Append(foot);
    }

    private StringBuilder RenderList(ListSection list, DocumentStyle style)
    {
        var s = _stylesManager
            .ApplyStyleId(style, list.ListStyleId)
            .ApplyStyleDefinition(list.ListStyle);

        var head = "<div style=\"overflow:auto;\">";

        var bulletHead = "<div style=\"float: left; width: 10%;\">";
        var bullet = RenderBullet(s);
        var bulletFoot = "</div>\n";

        var indentedHead = "<div style=\"float: right; width: 90%;\">";
        var indentedContet = RenderIndentedContent(list.FirstParagraph, list.Sections, style);
        var indetedFoot = "</div>\n";

        var foot = "</div>\n";

        return new StringBuilder()
            .Append(head)
            .Inspect($"ListStyleId: {list.ListStyleId.Match(s => s.Id, "None")}")
            .Inspect($"Indentation: {s.ListStyle.Indentation}")
            .Append(bulletHead)
            .Append(bullet)
            .Append(bulletFoot)
            .Append(indentedHead)
            .Append(indentedContet)
            .Append(indetedFoot)
            .Append(foot);
    }

    private StringBuilder RenderIndentedContent(
        ParagraphSection firstParagraph, IList<DocumentSection> sections,
        DocumentStyle style)
    {
        var paragraph = RenderParagraph(firstParagraph, style);

        var sect = sections
            .Select(s => RenderDocSection(s, style))
            .Fold(new StringBuilder(), (a, sb) => a.Append(sb));

        return new StringBuilder().Append((StringBuilder?) paragraph)
            .Append(sect);
    }

    private string RenderBullet(DocumentStyle s) => s.ListStyle.Bullet;

    private StringBuilder RenderParagraph(ParagraphSection par, DocumentStyle style)
    {
        var st = style.ApplyStyleDefinition(par.ParagraphStyle);

        var head = "<p style=\"margin: 0px\">";
        var cont = par.Spans
            .Select<CharacterSpan, StringBuilder>(s => RenderSpan(s, st))
            .Fold(new StringBuilder(), (a, sb) => a.Append(sb));

        var foot = "</p>\n";

        return new StringBuilder()
            .Append(head)
            .Inspect($"LineSpacing: {st.LineSpacing}")
            .Inspect($"Below: {st.SpacingBelow}")
            .Append(cont)
            .Append(foot);
    }

    private StringBuilder RenderSpan(CharacterSpan characterSpan, DocumentStyle style)
    {
        var data = DocRenderer.RenderSpan(characterSpan);
        var stubble = new StubbleBuilder().Build();
        string output = stubble.Render("<span" +
                                    "{{#Class}} class=\"{{Class}}\"{{/Class}}" +
                                    "{{#Style}} style=\"" +
                                    "{{#FontFamily}}font-family:{{FontFamily}};{{/FontFamily}}" +
                                    "{{#FontSize}}font-size:{{FontSize}};{{/FontSize}}" +
                                    "\"{{/Style}}" +
                                    ">{{Characters}}</span>", data);

        return new StringBuilder(output);
    }
}