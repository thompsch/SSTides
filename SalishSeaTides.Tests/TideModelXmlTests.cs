using System.Xml.Serialization;
using SalishSeaTides.Models;
using Xunit;

namespace SalishSeaTides.Tests;

public class TideModelXmlTests
{
    private static TideModel Deserialize(string xml)
    {
        var serializer = new XmlSerializer(typeof(TideModel));
        using var reader = new StringReader(xml);
        return (TideModel)serializer.Deserialize(reader)!;
    }

    [Fact]
    public void TideModel_DeserializesStationMetadata()
    {
        var xml = """
            <?xml version="1.0" encoding="UTF-8" ?>
            <datainfo>
                <stationid>9447856</stationid>
                <stationname>Sandy Point</stationname>
                <data></data>
            </datainfo>
            """;

        var model = Deserialize(xml);

        Assert.Equal("9447856", model.StationId);
        Assert.Equal("Sandy Point", model.StationName);
    }

    [Fact]
    public void TideModel_DeserializesSingleItem()
    {
        var xml = """
            <?xml version="1.0" encoding="UTF-8" ?>
            <datainfo>
                <stationid>9447856</stationid>
                <stationname>Sandy Point</stationname>
                <data>
                    <item>
                        <date>2024/01/01</date>
                        <day>Mon</day>
                        <time>06:17 AM</time>
                        <pred_in_ft>4.5</pred_in_ft>
                        <pred_in_cm>137</pred_in_cm>
                        <highlow>H</highlow>
                    </item>
                </data>
            </datainfo>
            """;

        var model = Deserialize(xml);

        Assert.Single(model.Items);
        Assert.Equal("2024/01/01", model.Items[0].Date);
        Assert.Equal("06:17 AM", model.Items[0].Time);
        Assert.Equal("4.5", model.Items[0].PredInFt);
        Assert.Equal("H", model.Items[0].HighLow);
    }

    [Fact]
    public void TideModel_DeserializesMultipleItems()
    {
        var xml = """
            <?xml version="1.0" encoding="UTF-8" ?>
            <datainfo>
                <stationid>9447856</stationid>
                <stationname>Sandy Point</stationname>
                <data>
                    <item>
                        <date>2024/01/01</date>
                        <day>Mon</day>
                        <time>06:17 AM</time>
                        <pred_in_ft>4.5</pred_in_ft>
                        <pred_in_cm>137</pred_in_cm>
                        <highlow>H</highlow>
                    </item>
                    <item>
                        <date>2024/01/01</date>
                        <day>Mon</day>
                        <time>12:44 PM</time>
                        <pred_in_ft>-0.3</pred_in_ft>
                        <pred_in_cm>-9</pred_in_cm>
                        <highlow>L</highlow>
                    </item>
                </data>
            </datainfo>
            """;

        var model = Deserialize(xml);

        Assert.Equal(2, model.Items.Count);
        Assert.Equal("H", model.Items[0].HighLow);
        Assert.Equal("L", model.Items[1].HighLow);
        Assert.Equal("-0.3", model.Items[1].PredInFt);
    }

    [Fact]
    public void DataItem_StoresAllFields()
    {
        var xml = """
            <?xml version="1.0" encoding="UTF-8" ?>
            <datainfo>
                <stationid>9449880</stationid>
                <stationname>Friday Harbor</stationname>
                <data>
                    <item>
                        <date>2024/06/15</date>
                        <day>Sat</day>
                        <time>09:30 AM</time>
                        <pred_in_ft>7.8</pred_in_ft>
                        <pred_in_cm>238</pred_in_cm>
                        <highlow>H</highlow>
                    </item>
                </data>
            </datainfo>
            """;

        var model = Deserialize(xml);
        var item = model.Items[0];

        Assert.Equal("2024/06/15", item.Date);
        Assert.Equal("Sat", item.Day);
        Assert.Equal("09:30 AM", item.Time);
        Assert.Equal("7.8", item.PredInFt);
        Assert.Equal("238", item.PredInCm);
        Assert.Equal("H", item.HighLow);
    }
}
