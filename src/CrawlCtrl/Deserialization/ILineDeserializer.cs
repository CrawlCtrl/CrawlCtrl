namespace CrawlCtrl.Deserialization
{
    internal interface ILineDeserializer<out TLine> where TLine : Line
    {
        TLine Deserialize(string value, string comment);
    }
}