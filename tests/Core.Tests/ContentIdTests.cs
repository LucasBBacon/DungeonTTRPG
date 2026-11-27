using Dungeon.Core.Content;
using Xunit;

namespace Dungeon.Core.Tests;

public class ContentIdTests
{
    [Theory]
    [InlineData("boss.old_ragged")]
    [InlineData("a:b-c_1")]
    public void Valid_ids_parse(string s)
    {
        Assert.True(ContentId.IsValid(s));
        Assert.Equal(s, ContentId.Parse(s).Value);
        Assert.Equal(s, ContentId.Parse(s).ToString());
    }

    [Theory]
    [InlineData("")]
    [InlineData("Boss")]
    [InlineData("a b")]
    [InlineData("é")]
    [InlineData("a/b")]
    public void Invalid_ids_are_rejected(string s)
    {
        Assert.False(ContentId.IsValid(s));
        Assert.Throws<ArgumentException>(() => ContentId.Parse(s));
    }

    [Fact]
    public void Null_is_invalid()
    {
        Assert.False(ContentId.IsValid(null!));
        Assert.Throws<ArgumentException>(() => ContentId.Parse(null!));
    }

    [Fact]
    public void Parse_message_names_the_bad_character()
    {
        var ex = Assert.Throws<ArgumentException>(() => ContentId.Parse("ab/c"));
        Assert.Contains("'/'", ex.Message);
        Assert.Contains("index 2", ex.Message);
    }
}
