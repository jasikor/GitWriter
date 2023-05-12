// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Text;
using System.Text.Json;
using BookModel.TextDocument;
using BookModel.TextDocument.StyleDefinitions;
using BookModel.TextDocument.Styles;
using LanguageExt;

namespace Sandbox;

public static class Program
{
    public static int Main(string[] args)
    {
        var doc = CreateDocument();

        SaveDocOnDesktopAsJson(doc);

        var defaultStyle = GetDefaultStyle();
        var styleManager = new StylesManager();

        var docHead = GetHead();
        var docBody = Render(doc, defaultStyle, styleManager);
        var docFoot = GetFoot();

        var res = new StringBuilder()
            .Append(docHead)
            .Append(docBody)
            .Append(docFoot);


        SaveRendered(res);
        return 0;
    }

    private static string GetFoot() => "</body></html>";

    private static string GetHead() =>
        $"<!DOCTYPE html><html> <head><title>Page Title {DateTime.Now}</title></head>" +
        $"<body  style=\"font-family:iA Writer Quattro V Regular, Courier New\">\n";

    private static DocumentStyle GetDefaultStyle()
    {
        return new StyleBuilder()
            .AboveSpacing(2)
            .BelowSpacing(13)
            .LineSpacing(15f)
            .CharacterStyle(new CharacterStyle() {FontFamily = "Arial", FontSize = 12})
            .ListStyle(new ListStyle() {Indentation = 20f})
            .Build();
    }

    private static void SaveRendered(StringBuilder res)
    {
        Console.WriteLine(res);
        File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\gupie.html",
            res.ToString());
    }

    private static void SaveDocOnDesktopAsJson(Document doc)
    {
        var serialized = JsonSerializer.Serialize(doc,
            new JsonSerializerOptions {WriteIndented = true, IncludeFields = true, MaxDepth = 10});
        File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\gupie.json",
            serialized);
    }

    private static StringBuilder Render(Document doc, DocumentStyle defaultStyle, StylesManager stylesManager)
    {
        StringBuilder res = new StringBuilder();
        res.Append(Inspect(defaultStyle.ToString()));

        foreach (var d in doc.Items) {
            res.Append(RenderDocSection(d, defaultStyle, stylesManager));
            res.Append("\n");
        }

        return res;
    }

    private static string Inspect(string s) =>
        $"<p style=\"font-family:iA Writer Quattro V Regular, Courier New; font-size: 8pt; " +
        $"background-color: #f8f8f8; margin: 1px\">{s}</p>";

    private static StringBuilder RenderDocSection(DocumentSection documentSection, DocumentStyle style,
        StylesManager stylesManager)
    {
        var s = stylesManager
            .ApplyStyleId(style, documentSection.ParagraphStyleId)
            .ApplyStyleDefinition(documentSection.VerticalSpacing);

        var res = new StringBuilder();
        res.Append(Inspect($"ParagraphStyleId: {documentSection.ParagraphStyleId.Match(s => s.Id, "None")}"));
        res.Append(Inspect($"Above: {s.SpacingAbove}"));

        res.Append($"<div style=\"margin: {style.SpacingAbove}px 0px {style.LineSpacing}px \">");
        res.Append(documentSection switch {
            ParagraphSection par => RenderParagraph(par, s, stylesManager),
            ListSection list => RenderList(list, s, stylesManager),
            _ => new UnreachableException()
        });

        res.Append("</div>");

        return res;
    }

    private static StringBuilder RenderList(ListSection list, DocumentStyle style, StylesManager stylesManager)
    {
        var res = new StringBuilder();
        res.Append("<div style=\"overflow:auto;\">");

        var s = stylesManager
            .ApplyStyleId(style, list.ListStyleId)
            .ApplyStyleDefinition(list.ListStyle);
        res.Append("<div style=\"float: left; width: 10%;\">");
        res.Append(Inspect($"ListStyleId: {list.ListStyleId.Match(s => s.Id, "None")}"));
        res.Append(Inspect($"Indentation: {s.ListStyle.Indentation}"));
        res.Append(RenderBullet());
        res.Append("</div>");

        res.Append("<div style=\"float: right; width: 90%;\">");
        res.Append(RenderIntendedContent(list.FirstParagraph, list.Sections, style, stylesManager));
        res.Append("</div>");

        res.Append("</div>");
        return res;
    }

    private static StringBuilder RenderIntendedContent(
        ParagraphSection firstParagraph, IList<DocumentSection> sections,
        DocumentStyle style, StylesManager stylesManager)
    {
        var res = new StringBuilder();

        res.Append(RenderParagraph(firstParagraph, style, stylesManager));


        foreach (var section in sections) {
            res.Append(RenderDocSection(section, style, stylesManager));
        }

        return res;
    }

    private static string RenderBullet() => "\x2022";

    private static StringBuilder RenderParagraph(ParagraphSection par, DocumentStyle style,
        StylesManager stylesManager)
    {
        var st = style.ApplyStyleDefinition(par.ParagraphStyle);
        var res = new StringBuilder();
        res.Append("<p style=\"margin: 0px\">");
        res.Append(Inspect($"LineSpacing: {st.LineSpacing}"));
        foreach (var s in par.Spans)
            res.Append(RenderSpan(s, st, stylesManager));

        res.Append("</p>");
        res.Append(Inspect($"Below: {st.SpacingBelow}"));

        return res;
    }

    private static StringBuilder RenderSpan(CharacterSpan characterSpan, DocumentStyle style,
        StylesManager stylesManager)
    {
        var s = stylesManager
            .ApplyStyleId(style, characterSpan.CharacterStyleDefinitionId)
            .ApplyStyleDefinition(characterSpan.CharacterStyle);
        var res = new StringBuilder();
        res.Append(Inspect($"CharacterStyleId: {characterSpan.CharacterStyleDefinitionId.Match(s => s.Id, "None")}"));

        res.Append(Inspect($"Character:{s.CharacterStyle}"));
        res.Append($"<span style=\"font-family:{s.CharacterStyle.FontFamily}\">{characterSpan.Characters}</span>");
        return res;
    }

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
            LineSpacing = random.NextSingle() * 15f + 100f   ,
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

    private static ParagraphSection CreateParagraphSection()
    {
        var res = new ParagraphSection() {ParagraphStyleId = ParagraphStyleDefinitionID.Default};
        res.Spans.Add(CreateCharacterSpan("Span pierwszy"));
        res.Spans.Add(CreateCharacterSpan(" i tu drugi"));
        res.ParagraphStyle =
            random.NextSingle() < 0.5
                ? new() {
                    LineSpacing = random.NextSingle() * 15f,
                    SpacingBelow = Option<float>.None, //random.NextSingle() * 10f,
                    SpacingAbove = 3.3f,
                    
                }
                : new();
        return res;
    }

    private static CharacterSpan CreateCharacterSpan(string s)
    {
        var res = new CharacterSpan();
        res.Characters = s;
        res.CharacterStyle =
            random.NextSingle() < 0.5
                ? new() {
                    FontSize = random.NextSingle() * 15 + 50,
                    FontFamily = random.NextSingle() < 0.5f ? "Times New Roman" : "Courier New",
                }
                : new();
        return res;
    }
}