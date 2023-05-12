using System.Diagnostics;
using System.Text;
using BookModel.TextDocument;
using BookModel.TextDocument.StyleDefinitions;
using BookModel.TextDocument.Styles;
using LanguageExt;

namespace Sandbox;

public static class Program
{
    public static void Main(string[] args)
    {
        var doc = CreateDocument();

        var defaultStyle = GetDefaultStyle();
        var styleManager = new StylesManager();

        var docHead = $"<!DOCTYPE html><html> <head><title>Page Title {DateTime.Now}</title></head>\n" +
                      $"<body  style=\"font-family:iA Writer Quattro V Regular, Courier New\">\n";
        var docBody = Render(doc, defaultStyle, styleManager);
        var docFoot = "</body>\n</html>";

        var res = new StringBuilder()
            .Append(docHead)
            .Append(docBody)
            .Append(docFoot);

        SaveRendered(res);
    }

    private static DocumentStyle GetDefaultStyle() =>
        new StyleBuilder()
            .AboveSpacing(2)
            .BelowSpacing(13)
            .LineSpacing(15f)
            .CharacterStyle(new CharacterStyle() {FontFamily = "Arial", FontSize = 12})
            .ListStyle(new ListStyle() {Indentation = 20f})
            .Build();

    private static void SaveRendered(StringBuilder res)
    {
        Console.WriteLine(res);
        File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\gupie.html",
            res.ToString());
    }

    /**********************************************************************************/

    private static StringBuilder Render(Document doc, DocumentStyle defaultStyle, StylesManager stylesManager)
    {
        var sections = doc.Items
            .Select(d => RenderDocSection(d, defaultStyle, stylesManager))
            .Fold(new StringBuilder(), (a, sb) => a.Append(sb));

        return new StringBuilder()
            .Inspect(defaultStyle.ToString())
            .Append(sections);
    }


    private static StringBuilder RenderDocSection(DocumentSection documentSection, DocumentStyle style,
        StylesManager stylesManager)
    {
        var s = stylesManager
            .ApplyStyleId(style, documentSection.ParagraphStyleId)
            .ApplyStyleDefinition(documentSection.VerticalSpacing);

        var head = $"<div style=\"margin: {style.SpacingAbove}px 0px {style.LineSpacing}px \">";

        object content = documentSection switch {
            ParagraphSection par => RenderParagraph(par, s, stylesManager),
            ListSection list => RenderList(list, s, stylesManager),
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

    private static StringBuilder RenderList(ListSection list, DocumentStyle style, StylesManager stylesManager)
    {
        var s = stylesManager
            .ApplyStyleId(style, list.ListStyleId)
            .ApplyStyleDefinition(list.ListStyle);

        var head = "<div style=\"overflow:auto;\">";

        var bulletHead = "<div style=\"float: left; width: 10%;\">";
        var bullet = RenderBullet(s);
        var bulletFoot = "</div>\n";

        var indentedHead = "<div style=\"float: right; width: 90%;\">";
        var indentedContet = RenderIndentedContent(list.FirstParagraph, list.Sections, style, stylesManager);
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

    private static StringBuilder RenderIndentedContent(
        ParagraphSection firstParagraph, IList<DocumentSection> sections,
        DocumentStyle style, StylesManager stylesManager)
    {
        var paragraph = RenderParagraph(firstParagraph, style, stylesManager);

        var sect = sections
            .Select(s => RenderDocSection(s, style, stylesManager))
            .Fold(new StringBuilder(), (a, sb) => a.Append(sb));

        return new StringBuilder()
            .Append(paragraph)
            .Append(sect);
    }

    private static string RenderBullet(DocumentStyle s) => s.ListStyle.Bullet;

    private static StringBuilder RenderParagraph(ParagraphSection par, DocumentStyle style,
        StylesManager stylesManager)
    {
        var st = style.ApplyStyleDefinition(par.ParagraphStyle);

        var head = "<p style=\"margin: 0px\">";
        var cont = par.Spans
            .Select(s => RenderSpan(s, st, stylesManager))
            .Fold(new StringBuilder(), (a, sb) => a.Append(sb));

        var foot = "</p>\n";

        return new StringBuilder()
            .Append(head)
            .Inspect($"LineSpacing: {st.LineSpacing}")
            .Inspect($"Below: {st.SpacingBelow}")
            .Append(cont)
            .Append(foot);
    }

    private static StringBuilder RenderSpan(CharacterSpan characterSpan, DocumentStyle style,
        StylesManager stylesManager)
    {
        var s = stylesManager
            .ApplyStyleId(style, characterSpan.CharacterStyleDefinitionId)
            .ApplyStyleDefinition(characterSpan.CharacterStyle);

        var head = $"<span style=\"font-family:{s.CharacterStyle.FontFamily}\">";
        var cont = characterSpan.Characters;
        var foot = $"</span>";

        return new StringBuilder()
            .Inspect($"CharacterStyleId: {characterSpan.CharacterStyleDefinitionId.Match(s => s.Id, "None")}")
            .Inspect($"Character:{s.CharacterStyle}")
            .Append(head)
            .Append(cont)
            .Append(foot);
    }

    /**************************************************************************************/

    private static StringBuilder Inspect(this StringBuilder sb, string s) =>
        sb.Append(
            $"<p style=\"font-family:iA Writer Quattro V Regular, Courier New; font-size: 8pt; " +
            $"background-color: #f8f8f8; margin: 1px\">{s}</p>\n");

/*******************************************************************************************/
    private static Document CreateDocument()
    {
        var res = new Document();
        for (int i = 0; i < 2; i++)
            res.Items.Add(CreateParagraphSection());

        for (int i = 0; i < 2; i++)
            res.Items.Add(CreateListSection());

        for (int i = 0; i < 2; i++)
            res.Items.Add(CreateParagraphSection());

        return res;
    }

    private static readonly Random random = new Random();

    private static ListSection CreateListSection()
    {
        var res = new ListSection() {
            ParagraphStyleId = ParagraphStyleDefinitionID.Heading1,
            ListStyleId = ListStyleDefinitionID.Default
        };
        res.FirstParagraph.Spans.Add(CreateCharacterSpan("pierwszy paragraf listy "));
        res.FirstParagraph.Spans.Add(CreateCharacterSpan("drugi span pierwszego paragrafu listy"));
        res.FirstParagraph.ParagraphStyle = new() {
            LineSpacing = random.NextSingle() * 15f + 100f,
            SpacingAbove = 3.3f,
            SpacingBelow = 4.4f,
        };
        res.ListStyle = random.NextSingle() < 0.5
            ? new() {Indentation = random.NextSingle() * 30f}
            : new();
        for (int i = random.Next(0, 9); i > 0; i--)
            res.Sections.Add(random.NextSingle() < 0.8
                ? CreateParagraphSection()
                : CreateListSection());
        return res;
    }

    private static ParagraphSection CreateParagraphSection() =>
        new ParagraphSectionBuilder()
            .AddCharacterSpan(CreateCharacterSpan("Span pierwszy"))
            .AddCharacterSpan(CreateCharacterSpan(" i tu drugi"))
            .ParagraphStyle(
                random.NextSingle() < 0.5
                    ? new() {
                        LineSpacing = random.NextSingle() * 15f,
                        SpacingBelow = Option<float>.None, //random.NextSingle() * 10f,
                        SpacingAbove = 3.3f,
                    }
                    : new())
            .ParagraphStyleId( ParagraphStyleDefinitionID.Default)
            .Build();

    private static CharacterSpan CreateCharacterSpan(string s) =>
        new CharacterSpan {
            Characters = s,
            CharacterStyle = random.NextSingle() < 0.5
                ? new() {
                    FontSize = random.NextSingle() * 15 + 50,
                    FontFamily = random.NextSingle() < 0.5f ? "Times New Roman" : "Courier New",
                }
                : new(),
            CharacterStyleDefinitionId = CharacterStyleDefinitionID.Default
        };
}