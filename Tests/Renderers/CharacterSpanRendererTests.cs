using BookModel.Renderers;
using BookModel.TextDocument;
using BookModel.TextDocument.StyleDefinitions;
using FluentAssertions;

namespace Tests.Renderers;

public class CharacterSpanRendererTests
{
    [Theory]
    [InlineData("")]
    [InlineData("DefaultStyle")]
    public static void RenderSpan_Renders_Class_WhenStyleID_IsPresent(string style)
    {
        var characterSpan = new CharacterSpan() {
            CharacterStyleDefinitionId = new CharacterStyleDefinitionID() {Id = style}
        };
        var actual = DocRenderer
            .RenderSpan(characterSpan)
            .Class as string;
        actual.Should().Be(style);
    }

    [Fact]
    public static void RenderSpan_Renders_Class_WhenStyleID_IsAbsent()
    {
        // Arrange
        var characterSpan = new CharacterSpan() { };
        // Act
        var actual = DocRenderer
            .RenderSpan(characterSpan)
            .Class as string;
        // Assert        
        actual.Should().Be(string.Empty);
    }

    [Theory]
    [InlineData("")]
    [InlineData("asdf")]
    public static void RenderSpan_Renders_Characters_Correctly(string expected)
    {
        var characterSpan = new CharacterSpan() {
            Characters = expected,
        };
        var actual = DocRenderer
            .RenderSpan(characterSpan)
            .Characters as string;
        actual.Should().Be(expected);
    }

    [Fact]
    public static void RenderSpan_Renders_EmptyStyle_AsEmpty()
    {
        var characterSpan = new CharacterSpan() { };
        var actual = DocRenderer
            .RenderSpan(characterSpan)
            .Style as string;
        actual.Should().Be(string.Empty);
    }


    public static IEnumerable<object[]> GetVariousFields()
    {
        yield return new object[] {new CharacterStyleDefinition() { }, "", ""};
        yield return new object[] {
            new CharacterStyleDefinition() {
                FontFamily = "expectedFontFamily",
                FontSize = 13,
            },
            "expectedFontFamily",
            "13"
        };
        yield return new object[] {
            new CharacterStyleDefinition() {
                FontSize = 13,
            },
            "",
            "13"
        };
        yield return new object[] {
            new CharacterStyleDefinition() {
                FontFamily = "expectedFontFamily",
            },
            "expectedFontFamily",
            ""
        };
    }

    [Theory]
    [MemberData(nameof(GetVariousFields))]
    public static void RenderSpan_Renders_Style_With_VariousFieldsConfiguration(
        CharacterStyleDefinition style, string expectedFontFamily, string expectedFontSize)
    {
        var characterSpan = new CharacterSpan() {
            CharacterStyle = style
        };
        var actual = DocRenderer
            .RenderSpan(characterSpan)
            .Style;
        (actual.FontFamily as string).Should().Be(expectedFontFamily);
        (actual.FontSize as string).Should().Be(expectedFontSize.ToString());
    }
}