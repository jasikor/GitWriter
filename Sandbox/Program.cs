﻿// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Text;
using BookModel.TextDocument;
using BookModel.TextDocument.Styles;
using LanguageExt;

namespace Sandbox;

public static class Program
{
    public static int Main(string[] args)
    {
        var doc = CreateDocument();
        var defaultStyleBuilder = new StyleBuilder();
        var defaultStyle = defaultStyleBuilder
            .AboveSpacing(2)
            .BelowSpacing(13)
            .LineSpacing(15f)
            .Font(new FontStyle() {Family = "Arial", Size = 12})
            .ListStyle(new ListStyle() {Indentation = 20f})
            .Build();
        ;
        var styleManager = new StylesManager();

        var res = new StringBuilder();
        res.Append(
            $"<!DOCTYPE html><html> <head><title>Page Title {DateTime.Now}</title></head><body  style=\"font-family:iA Writer Quattro V Regular, Courier New\">\n");

        res.Append(Render(doc, defaultStyle, styleManager));


        res.Append("</body></html>");
        Console.WriteLine(res);
        File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\gupie.html",
            res.ToString());
        return 0;
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
        $"<p style=\"font-family:iA Writer Quattro V Regular, Courier New; font-size: 8pt; background-color: #f8f8f8; margin: 1px\">{s}</p>";

    private static StringBuilder RenderDocSection(DocumentSection documentSection, DocumentStyle style,
        StylesManager stylesManager)
    {
        var s = stylesManager
            .Apply(style, documentSection.ParagraphStyleId)
            .Apply(documentSection.VerticalSpacing);
        var res = new StringBuilder();
        res.Append(Inspect($"Above: {s.SpacingAbove}"));

        res.Append($"<div style=\"margin: {style.SpacingAbove}px 0px {style.LineSpacing}px \">");
        res.Append(documentSection switch {
            ParagraphSection par => RenderParagraph(par, s),
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

        var s = stylesManager.Apply(style,list.ListStyleId);
        res.Append("<div style=\"float: left; width: 10%;\">");
        res.Append(Inspect($"Indentation: {s.ListStyle.Indentation}"));
        res.Append(RenderBullet());
        res.Append("</div>");

        res.Append("<div style=\"float: right; width: 90%;\">");
        res.Append(RenderIntendedContent(list, style, stylesManager));
        res.Append("</div>");

        res.Append("</div>");
        return res;
    }

    private static StringBuilder RenderIntendedContent(ListSection list, DocumentStyle style, StylesManager stylesManager)
    {
        var s = style.Apply(list.ListStyle);

        var res = new StringBuilder();
        res.Append(Inspect($"{s.ListStyle}, Below:{s.SpacingBelow}"));

        res.Append(RenderParagraph(list.FirstParagraph, s));


        foreach (var section in list.Sections) {
            res.Append(RenderDocSection(section, style, stylesManager));
        }

        return res;
    }

    private static string RenderBullet() => "\x2022";

    private static StringBuilder RenderParagraph(ParagraphSection par, DocumentStyle style)
    {
        var st = style.Apply(par.ParagraphStyle);
        var res = new StringBuilder();
        res.Append("<p style=\"margin: 0px\">");
        res.Append(Inspect($"LineSpacing: {st.LineSpacing}"));
        foreach (var s in par.Spans)
            res.Append(RenderSpan(s, st));

        res.Append("</p>");
        res.Append(Inspect($"Below: {st.SpacingBelow}"));

        return res;
    }

    private static StringBuilder RenderSpan(CharacterSpan characterSpan, DocumentStyle style)
    {
        var s = style.Apply(characterSpan.Style);
        var res = new StringBuilder();
        res.Append(Inspect($"Font:{s.Font}"));
        res.Append($"<span style=\"font-family:{s.Font.Family}\">{characterSpan.Characters}</span>");
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
        var res = new ListSection();
        res.FirstParagraph.Spans.Add(CreateCharacterSpan("pierwszy paragraf listy "));
        res.FirstParagraph.Spans.Add(CreateCharacterSpan("drugi span pierwszego paragrafu listy"));
        res.FirstParagraph.ParagraphStyle = new() {LineSpacing = random.NextSingle() * 15f + 100f};
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
        var res = new ParagraphSection();
        res.Spans.Add(CreateCharacterSpan("Span pierwszy"));
        res.Spans.Add(CreateCharacterSpan(" i tu drugi"));
        res.ParagraphStyle =
            random.NextSingle() < 0.5
                ? new() {
                    LineSpacing = random.NextSingle() * 15f,
                    SpacingBelow = Option<float>.None //random.NextSingle() * 10f,
                }
                : new();
        return res;
    }

    private static CharacterSpan CreateCharacterSpan(string s)
    {
        var res = new CharacterSpan();
        res.Characters = s;
        res.Style =
            random.NextSingle() < 0.5
                ? new() {
                    Size = random.NextSingle() * 15 + 50,
                    Family = random.NextSingle() < 0.5f ? "Times New Roman" : "Courier New",
                }
                : new();
        return res;
    }
}