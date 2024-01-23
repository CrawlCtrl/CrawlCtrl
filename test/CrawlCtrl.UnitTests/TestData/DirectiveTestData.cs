namespace CrawlCtrl.UnitTests.TestData;

internal static class DirectiveTestData
{
    public static IEnumerable<object[]> OriginalDirectiveTestData =>
        new List<object[]>
        {
            new object[] { "Directive", "Directive" },
            new object[] { " Directive", " Directive" },
            new object[] { "Directive ", "Directive " },
            new object[] { " Directive ", " Directive " }
        };
    
    public static IEnumerable<object[]> TrimmedDirectiveTestData =>
        new List<object[]>
        {
            new object[] { "Directive", "Directive" },
            new object[] { " Directive", "Directive" },
            new object[] { "Directive ", "Directive" },
            new object[] { " Directive ", "Directive" }
        };
    
    public static IEnumerable<object[]> EmptyDirectiveTestData =>
        new List<object[]>
        {
            new object[] { "" },
            new object[] { "  " }
        };
}