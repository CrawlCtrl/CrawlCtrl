namespace CrawlCtrl.UnitTests.LineTypes;

internal static class CommentTestData
{
    public static IEnumerable<object[]> OriginalCommentTestData =>
        new List<object[]>
        {
            new object[] { "", "" },
            new object[] { "  ", "  " },
            new object[] { "Some awesome comment", "Some awesome comment" },
            new object[] { " Some awesome comment", " Some awesome comment" },
            new object[] { "Some awesome comment ", "Some awesome comment " },
            new object[] { " Some awesome comment ", " Some awesome comment " }
        };
    
    public static IEnumerable<object[]> TrimmedCommentTestData =>
        new List<object[]>
        {
            new object[] { "", "" },
            new object[] { "  ", "" },
            new object[] { "Some awesome comment", "Some awesome comment" },
            new object[] { " Some awesome comment", "Some awesome comment" },
            new object[] { "Some awesome comment ", "Some awesome comment" },
            new object[] { " Some awesome comment ", "Some awesome comment" }
        };
}