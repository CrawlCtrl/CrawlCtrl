namespace CrawlCtrl.Deserialization
{
    internal interface ILineDeserializer<out TLine> where TLine : Line
    {
        TLine Deserialize(string directive, string value, string comment);
    }
}